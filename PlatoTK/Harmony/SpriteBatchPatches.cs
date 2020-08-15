﻿using Harmony;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

            foreach (MethodInfo method in typeof(SpriteBatch).GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m => m.Name.ToLower() == "draw"))
            {
                List<Type> parameterTypes = new List<Type>() { typeof(SpriteBatch).MakeByRefType(), typeof(Texture2D).MakeByRefType() };
                parameterTypes.AddRange(method.GetParameters().Where(pr => pr.Name.ToLower() != "texture").Select(p => p.ParameterType));
                if (typeof(SpriteBatchPatches).GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Static, Type.DefaultBinder, parameterTypes.ToArray(), new ParameterModifier[0]) is MethodInfo targetMethod)
                    harmony.Patch(method, new HarmonyMethod(targetMethod), null, null);
            }
        }

        internal static bool DrawFix(ref SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, Vector2 origin, float rotation = 0f, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
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
                    if (!(patch.Patch is Texture2D) || (!texture.Equals(patch.Texture()) && !(tag == patch.Id)))
                        continue;

                    _skipArea = true;
                    __instance.Draw(patch.Patch, destinationRectangle, patch.SourceArea, color, rotation, origin, effects, layerDepth);
                    _skipArea = false;
                    return false;
                }
            }

            return true;
        }

        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
                return DrawFix(ref __instance, ref texture, destinationRectangle, sourceRectangle, color, origin, rotation, effects, layerDepth);

            return true;
        }
        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
                return DrawFix(ref __instance, ref texture, destinationRectangle, sourceRectangle, color, Vector2.Zero);

            return true;
        }
        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                return DrawFix(ref __instance, ref texture, destinationRectangle, sourceRectangle, color, Vector2.Zero);
            }

            return true;
        }
        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
            {
                sourceRectangle = sourceRectangle.HasValue ? sourceRectangle.Value : new Rectangle(0, 0, texture.Width, texture.Height);
                return DrawFix(ref __instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(sourceRectangle.Value.Width * scale.X), (int)(sourceRectangle.Value.Height * scale.Y)), sourceRectangle, color, origin, rotation, effects, layerDepth);
            }

            return true;
        }
        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
            {
                sourceRectangle = sourceRectangle.HasValue ? sourceRectangle.Value : new Rectangle(0, 0, texture.Width, texture.Height);
                return DrawFix(ref __instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(sourceRectangle.Value.Width * scale), (int)(sourceRectangle.Value.Height * scale)), sourceRectangle, color, origin, rotation, effects, layerDepth);
            }

            return true;
        }
        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
            {
                sourceRectangle = sourceRectangle.HasValue ? sourceRectangle.Value : new Rectangle(0, 0, texture.Width, texture.Height);
                return DrawFix(ref __instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(sourceRectangle.Value.Width), (int)(sourceRectangle.Value.Height)), sourceRectangle, color, Vector2.Zero);
            }

            return true;
        }
        internal static bool Draw(ref SpriteBatch __instance, ref Texture2D texture, Vector2 position, Color color)
        {
            if (__instance is SpriteBatch && texture is Texture2D)
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                return DrawFix(ref __instance, ref texture, new Rectangle((int)(position.X), (int)(position.Y), (int)(texture.Width), (int)(texture.Height)), sourceRectangle, color, Vector2.Zero);
            }

            return true;
        }
    }
}