using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xTile;

namespace PlatoTK.Content
{
    public interface IMapHelper
    {
        Map PatchMapArea(Map map, Map patch, Point position, Rectangle? sourceArea = null, bool patchProperties = true, bool removeEmpty = false);
    }
}
