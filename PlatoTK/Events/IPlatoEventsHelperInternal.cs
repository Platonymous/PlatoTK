using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using xTile.Layers;

namespace PlatoTK.Events
{
    internal interface IPlatoEventsHelperInternal : IPlatoEventsHelper
    {

        void HandleTileAction(string[] commands, Farmer who, GameLocation location, Point position, Action<bool> callback);
    }
}
