using Microsoft.Xna.Framework;
using xTile;

namespace PlatoTK.Content
{
    internal class MapInjection : PatchableInjection<Map>
    {
        public MapInjection(
            IPlatoHelper helper,
            string assetName,
            Map value,
            InjectionMethod method,
            Rectangle? sourceArea = null,
            Rectangle? targetArea = null,
            string conditions = "")
            : base(helper, assetName, value, method, sourceArea, targetArea, conditions)
        {
        }
    }
}
