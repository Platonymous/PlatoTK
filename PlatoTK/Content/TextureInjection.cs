using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string conditions = "",
            IConditionsProvider provider = null)
            : base(helper, assetName, value, method, sourceArea, targetArea, conditions, provider)
        {
        }
    }
}
