using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using System;

namespace PlatoTK.Compat
{
    internal static class SpaceCorePatches
    {

        public static void PatchSpaceCore(IModHelper helper, Harmony instance)
        {
            if (!helper.ModRegistry.IsLoaded("spacechase0.SpaceCore"))
                return;

            Type TileSheetExtensions = Type.GetType("SpaceCore.TileSheetExtensions, SpaceCore");

            if (TileSheetExtensions != null)
            {
                instance.Patch(
                    original: AccessTools.Method(TileSheetExtensions, nameof(GetTileSheet), new[]{typeof(Texture2D), typeof(int)}),
                    postfix: new HarmonyMethod(AccessTools.Method(typeof(SpaceCorePatches), nameof(GetTileSheet))));
            }
        }

        public static void GetTileSheet(ref Texture2D __result, Texture2D tex, int index)
        {
            if (index != 0)
                __result.Name = "Extended:" + tex.Name + ":" + index;
        }
    }
}
