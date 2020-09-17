using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatoTK.Content
{
    internal class TextureInjection : PatchableInjection<Texture2D>
    {
        public TextureInjection(
            IPlatoHelper helper,
            string assetName,
            Texture2D value,
            InjectionMethod method,
            Rectangle? sourceArea = null,
            Rectangle? targetArea = null,
            string conditions = "")
            : base(helper, assetName, value, method, sourceArea, targetArea, conditions)
        {
        }
    }
}
