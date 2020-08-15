using Harmony;
using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatoTK.Harmony
{
    internal class GameLocationPatches
    {
        internal static bool _patched = false;
        internal static HashSet<ITileAction> TileActions = new HashSet<ITileAction>();

        internal static void InitializePatch()
        {
            if (_patched)
                return;

            _patched = true;
            var isActionableTile = AccessTools.Method(typeof(GameLocation), "isActionableTile");
            var tryToCheckAt = AccessTools.Method(typeof(Game1), "tryToCheckAt");
            var performTouchAction = AccessTools.Method(typeof(GameLocation), "performTouchAction");

            HarmonyInstance harmony = HarmonyInstance.Create($"Plato.TilePatches");
            harmony.Patch(isActionableTile, postfix: new HarmonyMethod(AccessTools.Method(typeof(GameLocationPatches), nameof(IsActionableTile))));
            harmony.Patch(tryToCheckAt, postfix: new HarmonyMethod(AccessTools.Method(typeof(GameLocationPatches), nameof(TryToCheckAt))));
            harmony.Patch(performTouchAction, postfix: new HarmonyMethod(AccessTools.Method(typeof(GameLocationPatches), nameof(PerformTouchAction))));
        }

        internal static bool TryCallTileAction(string action, string layer, int x, int y, GameLocation location)
        {
            string trigger = action.Split(' ')[0];
            bool flag = false;

            foreach (ITileAction tileAction in TileActions.Where( t => t.Trigger.Contains(trigger)))
            {
                flag = true;

                tileAction.Handler.Invoke(new TileActionTrigger(action, layer, x, y, location));
            }

            return flag;
        }

        internal static bool HasTileAction(string action)
        {
            string trigger = action.Split(' ')[0];
            return TileActions.Any(t => t.Trigger.Contains(trigger));
        }

        internal static void IsActionableTile(GameLocation __instance, ref bool __result, int xTile, int yTile)
        {
            if (__instance.doesTileHaveProperty(xTile, yTile, "Action", "Buildings") is string action && HasTileAction(action))
            {
                    Game1.isInspectionAtCurrentCursorTile = true;
                __result = true;
            }
        }

        internal static void TryToCheckAt(Vector2 grabTile, ref bool __result)
        {
            GameLocation location = Game1.currentLocation;
            if (!Utility.tileWithinRadiusOfPlayer((int)grabTile.X, (int)grabTile.Y, 1, Game1.player))
                return;

            if (Game1.currentLocation.doesTileHaveProperty((int)grabTile.X, (int)grabTile.Y, "Action", "Buildings") is string action 
                && TryCallTileAction(action, "Buildings", (int)grabTile.X, (int)grabTile.Y, location))
                __result = true;
        }

        internal static void PerformTouchAction(GameLocation __instance, string fullActionString, Vector2 playerStandingPosition)
        {
            TryCallTileAction(fullActionString, "Back", (int)playerStandingPosition.X, (int)playerStandingPosition.Y, __instance);
        }

    }
}
