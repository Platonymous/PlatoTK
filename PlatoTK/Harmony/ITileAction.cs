using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace PlatoTK.Harmony
{
    public interface ITileAction
    {
        string[] Trigger { get; }

        Action<ITileActionTrigger> Handler { get; }
    }

    public interface ITileActionTrigger
    {
        string Trigger { get; }

        string[] Params { get; }

        string Full { get; }

        string LayerName { get; }

        Point TileLocation { get; }

        GameLocation Location { get; }
    }
}
