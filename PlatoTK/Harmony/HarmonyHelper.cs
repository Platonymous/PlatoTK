using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatoTK.Network;
using StardewValley;

namespace PlatoTK.Harmony
{
    internal class HarmonyHelper : InnerHelper, IHarmonyHelper
    {
        public HarmonyInstance HarmonyInstance { get; }
        private static HashSet<MethodInfo> TracedMethods = new HashSet<MethodInfo>();
        internal static HashSet<TracedObject> TracedObjects;
        internal static HashSet<TypeForwarding> TracedTypes = new HashSet<TypeForwarding>();
        internal static HashSet<TypeForwarding> LinkedConstructors = new HashSet<TypeForwarding>();

        public HarmonyHelper(IPlatoHelper helper)
            : base(helper)
        {

            if (TracedObjects == null)
            {
                TracedObjects = new HashSet<TracedObject>();
                Helper.ModHelper.Events.GameLoop.ReturnedToTitle += GameLoop_ReturnedToTitle;
            }

            if (HarmonyInstance == null)
                HarmonyInstance = HarmonyInstance.Create($"Plato.HarmonyHelper.{Helper.ModHelper.ModRegistry.ModID}");
        }

        public void RegisterTileAction(ITileAction tileAction)
        {
            GameLocationPatches.InitializePatch();
            if (!GameLocationPatches.TileActions.Contains(tileAction))
                GameLocationPatches.TileActions.Add(tileAction);
        }

        public void RegisterTileAction(Action<ITileActionTrigger> handler, params string[] trigger)
        {
            RegisterTileAction(new TileAction(handler, trigger));
        }

        public Texture2D GetDrawHandle(string id, Func<ITextureDrawHandler, bool> handler, Texture2D texture)
        {
            SpriteBatchPatches.InitializePatch();
            return new PlatoTexture(id, handler, texture, texture?.GraphicsDevice);
        }

        public Texture2D GetDrawHandle(string id, Func<ITextureDrawHandler, bool> handler, int width, int height, GraphicsDevice graphicsDevice = null)
        {
            SpriteBatchPatches.InitializePatch();
            return new PlatoTexture(id, handler, width, height, graphicsDevice ?? Game1.graphics.GraphicsDevice);
        }

        public Texture2D GetDrawHandle<TData>(string id, TData data, Func<ITextureDrawHandler<TData>, bool> handler, Texture2D texture)
        {
            SpriteBatchPatches.InitializePatch();
            return new PlatoTexture<TData>(id, data, handler, texture, texture?.GraphicsDevice);
        }

        public Texture2D GetDrawHandle<TData>(string id, TData data, Func<ITextureDrawHandler, bool> handler, int width, int height, GraphicsDevice graphicsDevice = null)
        {
            SpriteBatchPatches.InitializePatch();
            return new PlatoTexture<TData>(id, data, handler, width, height, graphicsDevice ?? Game1.graphics.GraphicsDevice);
        }

        public void PatchTileDraw(string id, Func<Texture2D> targetTexture, Texture2D patch, Rectangle? sourceRectangle, int index, int tileWidth = 16, int tileHeight = 16)
        {
            id = $"{Helper.ModHelper.ModRegistry.ModID}.{id}";
            SpriteBatchPatches.DrawPatches.RemoveWhere(p => p.Id == id);
            PatchAreaDraw(id, targetTexture, patch, sourceRectangle, Game1.getSourceRectForStandardTileSheet(targetTexture(), index, tileWidth, tileHeight));
        }

        public void PatchAreaDraw(string id, Func<Texture2D> targetTexture, Texture2D patch, Rectangle? sourceRectangle, Rectangle? targetTileArea)
        {
            id = $"{Helper.ModHelper.ModRegistry.ModID}.{id}";
            SpriteBatchPatches.DrawPatches.RemoveWhere(p => p.Id == id);
            SpriteBatchPatches.DrawPatches.Add(new AreaDrawPatch(id, targetTexture, patch, targetTileArea.HasValue ? () => targetTileArea.Value : (Func<Rectangle>)null, sourceRectangle));
            SpriteBatchPatches.InitializePatch();
        }

        public void PatchTileDraw(string id, Func<Texture2D> targetTexture, Texture2D patch, Rectangle? sourceRectangle, Func<int> getIndex, int tileWidth = 16, int tileHeight = 16)
        {
            id = $"{Helper.ModHelper.ModRegistry.ModID}.{id}";
            SpriteBatchPatches.DrawPatches.RemoveWhere(p => p.Id == id);
            SpriteBatchPatches.DrawPatches.Add(new AreaDrawPatch(id, targetTexture, patch, () => Game1.getSourceRectForStandardTileSheet(targetTexture(), getIndex(), tileWidth, tileHeight), sourceRectangle));
            SpriteBatchPatches.InitializePatch();
        }

        public void PatchAreaDraw(string id, Func<Texture2D> targetTexture, Texture2D patch, Rectangle? sourceRectangle, Func<Rectangle> getTargetTileArea)
        {
            id = $"{Helper.ModHelper.ModRegistry.ModID}.{id}";
            SpriteBatchPatches.DrawPatches.RemoveWhere(p => p.Id == id);
            SpriteBatchPatches.DrawPatches.Add(new AreaDrawPatch(id, targetTexture, patch, getTargetTileArea, sourceRectangle));
            SpriteBatchPatches.InitializePatch();
        }

        public void RemoveDrawPatch(string id)
        {
            SpriteBatchPatches.DrawPatches.RemoveWhere(p => p.Id == id);
        }
        private void GameLoop_ReturnedToTitle(object sender, StardewModdingAPI.Events.ReturnedToTitleEventArgs e)
        {
            foreach (var obj in TracedObjects)
                if (obj.Target is ILinked linked)
                    linked.OnUnLink(Helper, obj.Original);

            TracedObjects.Clear();
        }

        public void UnlinkObjects(object original, object target = null)
        {
            TracedObjects.RemoveWhere(o =>
            {
                bool remove = (o.Original == original || original == null) && (o.Target == target || target == null);
                if(remove && o.Target is ILinked linked)
                        linked.OnUnLink(Helper, o.Original);

                return remove;
            });
        }

        public bool TryGetLink(object linkedObject, out object target)
        {
            if (TracedObjects.FirstOrDefault(t => t.Original == linkedObject) is TracedObject traced)
            {
                target = traced;
                return true;
            }
            target = null;
            return false;
        }
      
        public void LinkContruction<TOriginal, TTarget>()
        {
            if (LinkedConstructors.Any(l => l.FromType == typeof(TOriginal) && l.ToType == typeof(TTarget)))
                return;

            LinkedConstructors.Add(new TypeForwarding(typeof(TOriginal), typeof(TTarget), Helper, null));

            foreach (ConstructorInfo constructor in 
                typeof(TOriginal)
                .GetConstructors()
                .Where(c => typeof(TTarget)
                .GetConstructor(c.GetParameters().Select(p=>p.ParameterType).ToArray()) is ConstructorInfo))
            {
                List<Type> patchParameters = new List<Type>();
                patchParameters.Add(typeof(object).MakeByRefType());
                patchParameters.Add(typeof(MethodInfo));
                patchParameters.AddRange(constructor.GetParameters().Select(p => p.ParameterType.MakeByRefType()));

                HarmonyInstance.Patch(
                            original: constructor,
                            postfix: new HarmonyMethod(
                                type: typeof(ConstructorPatches),
                                name: nameof(ConstructorPatches.ConstructorPatch),
                                parameters: patchParameters.ToArray()
                            ));
            }
        }

        public void LinkTypes(Type originalType, Type targetType, object targetForAllInstances = null)
        {
            if (TracedTypes.Any(tt => tt.FromType == originalType && tt.ToType == targetType))
                return;

            TracedTypes.Add(new TypeForwarding(originalType, targetType, Helper, targetForAllInstances));

            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var declaredFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;

            foreach (MethodInfo tmethod in targetType.GetMethods(declaredFlags))
            {
                if (originalType.GetMethod(tmethod.Name, flags, Type.DefaultBinder, tmethod.GetParameters().Select(p => p.ParameterType).ToArray(), null) is MethodInfo method && method.GetParameters() is ParameterInfo[] parameters)
                {
                    if (TracedMethods.Contains(method))
                        continue;
                    else
                        TracedMethods.Add(method);

                    bool hasReturnType = method.ReturnType != Type.GetType("System.Void");
                    List<Type> patchParameters = new List<Type>();
                    List<Type> specificParameters = new List<Type>();

                    if (hasReturnType)
                        patchParameters.Add(typeof(object).MakeByRefType());

                    string patchName = hasReturnType ? nameof(MethodPatches.ForwardMethodPatch) : nameof(MethodPatches.ForwardMethodPatchVoid);

                    patchParameters.Add(typeof(object));
                    patchParameters.Add(typeof(MethodInfo));
                    patchParameters.AddRange(method.GetParameters().Select(p => typeof(object)));
                    specificParameters.AddRange(method.GetParameters().Select(p => p.ParameterType));

                    HarmonyMethod patchMethod = new HarmonyMethod(
                                type: typeof(MethodPatches),
                                name: patchName,
                                parameters: patchParameters.ToArray());

                    if (specificParameters.Count > 0)
                        if (AccessTools.GetDeclaredMethods(typeof(MethodPatches)).FirstOrDefault(m => m.Name == patchName && m.IsGenericMethod && m.GetParameters().Length == patchParameters.Count) is MethodInfo sMethod)
                            patchMethod = new HarmonyMethod(sMethod.MakeGenericMethod(specificParameters.ToArray()));

                    HarmonyInstance.Patch(
                        original: method,
                        prefix: patchMethod
                        );

                }
            }

        }

        public bool ForwardMethodVoid(object __instance, MethodInfo __originalMethod, params object[] args)
        {
            return MethodPatches.ForwardMethodVoid(__instance, __originalMethod, args);
        }

        public bool ForwardMethod(ref object __result, object __instance, MethodInfo __originalMethod, params object[] args)
        {
            return MethodPatches.ForwardMethod(ref __result, __instance, __originalMethod, args);
        }


        public bool LinkObjects(object original, object target)
        {
            if (original == null || target == null)
                return false;

            if (target is ILinked linked1 && !linked1.CanLinkWith(original))
                return false;

            if (target is ISyncedObject synced && synced.GetDataLink(original) is Netcode.NetString dataString)
                synced.Data = new SyncedData(dataString);

            if (target is ILinked linked2)
            {
                linked2.Link = new Link(original, target, Helper);
                linked2.OnLink(Helper, original);
            }

            if (TracedObjects.Any(t => t.Original == original && t.Target == target))
                return false;

            Type targetType = target.GetType();
            Type originalType = original.GetType();

            TracedObjects.Add(new TracedObject(original, target, Helper));

            LinkTypes(originalType, targetType, null);
            return true;
        }

        
    }
}
