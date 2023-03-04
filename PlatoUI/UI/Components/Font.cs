using BmFont;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlatoUI.UI.Components
{
    public class Font
    {
        public virtual FontFile FontFile { get; }

        public virtual Dictionary<char, FontChar> CharacterMap { get; }

        public virtual List<Texture2D> FontPages { get;}

        public Font(IPlatoUIHelper helper, string assetName)
        {
            FontFile = helper.UI.LoadFontFile(assetName);
            CharacterMap = helper.UI.ParseCharacterMap(FontFile);
            FontPages = helper.UI.LoadFontPages(FontFile, assetName);
        }
    }
}
