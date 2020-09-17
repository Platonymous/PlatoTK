using Microsoft.Xna.Framework;
using StardewValley;

namespace PlatoTK.Patching
{
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
