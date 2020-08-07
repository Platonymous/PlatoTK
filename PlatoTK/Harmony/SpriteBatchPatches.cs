using Harmony;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlatoTK.Harmony
{
    internal class SpriteBatchPatches
    {
        internal static bool _skipArea = false;

        internal static bool _patched = false;
        internal static HashSet<AreaDrawPatch> DrawPatches = new HashSet<AreaDrawPatch>();

        internal static void InitializePatch()
        {
            if (_patched)
                return;

            _patched = true;

            HarmonyInstance harmony = HarmonyInstance.Create($"Plato.DrawPatches");

            foreach (MethodInfo method in typeof(SpriteBatchPatches).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).Where(m => m.Name == "Draw"))
            {
                MethodInfo targetMethod = 
                    typeof(SpriteBatch)
                    .GetMethod(
                        "Draw", 
                        method.GetParameters().Select(p => p.ParameterType.Name.Contains("Texture2D") ? typeof(Texture2D) : p.ParameterType).Where(t => !t.Name.Contains("SpriteBatch"))
                        .ToArray());

                harmony.Patch(targetMethod, new HarmonyMethod(method), null, null);
            }
            }


        internal static bool DrawFix(SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, Vector2 origin, float rotation = 0f, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
        {
            if (texture == null)
                return true;

            if (texture is IPlatoTexture platoTexture && 
                !platoTexture.SkipHandler
                && platoTexture.CallTextureHandler(
                    __instance,
                    texture,
                    destinationRectangle,
                    sourceRectangle,
                    color, origin,
                    rotation, effects,
                    layerDepth))
                return false;

            if (!_skipArea)
            {
                string tag = texture.Tag is string s ? s : "";

                if (!sourceRectangle.HasValue)
                    sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

                foreach (var patch in DrawPatches.Where(p => sourceRectangle.Value == p.TargetArea() || (tag == p.Id)))
                {
                    if (!texture.Equals(patch.Texture()) && !(tag == patch.Id))
                        continue;

                    _skipArea = true;
                    __instance.Draw(patch.Patch, destinationRectangle, patch.SourceArea, color, rotation, origin, effects, layerDepth);
                    _skipArea = false;
                    return false;
                }
            }
            return true;
        }

        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            return DrawFix(__instance, ref texture, destinationRectangle, sourceRectangle, color, origin, rotation, effects, layerDepth);
        }
        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            return DrawFix(__instance, ref texture, destinationRectangle, sourceRectangle, color, Vector2.Zero);
        }
        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            return DrawFix(__instance, ref texture, destinationRectangle, sourceRectangle, color, Vector2.Zero);
        }
        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            sourceRectangle = sourceRectangle.HasValue ? sourceRectangle.Value : new Rectangle(0, 0, texture.Width, texture.Height);
            return DrawFix(__instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(sourceRectangle.Value.Width * scale.X), (int)(sourceRectangle.Value.Height * scale.Y)), sourceRectangle, color, origin, rotation, effects, layerDepth);
        }
        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            sourceRectangle = sourceRectangle.HasValue ? sourceRectangle.Value : new Rectangle(0, 0, texture.Width, texture.Height);

            return DrawFix(__instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(sourceRectangle.Value.Width * scale), (int)(sourceRectangle.Value.Height * scale)), sourceRectangle, color, origin, rotation, effects, layerDepth);
        }
        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            sourceRectangle = sourceRectangle.HasValue ? sourceRectangle.Value : new Rectangle(0, 0, texture.Width, texture.Height);
            return DrawFix(__instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(sourceRectangle.Value.Width), (int)(sourceRectangle.Value.Height)), sourceRectangle, color, Vector2.Zero);
        }
        internal static bool Draw(SpriteBatch __instance, ref Texture2D texture, Vector2 position, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            return DrawFix(__instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(texture.Width), (int)(texture.Height)), sourceRectangle, color, Vector2.Zero);

        }
    }
}
