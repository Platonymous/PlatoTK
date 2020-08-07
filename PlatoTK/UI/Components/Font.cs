using BmFont;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace PlatoTK.UI.Components
{
    public class Font
    {
        public virtual FontFile FontFile { get; set; } = null;

        public virtual Dictionary<char, FontChar> CharacterMap { get; set; } = null;

        public virtual List<Texture2D> FontPages { get; set; } = null;

        public Font(IPlatoHelper helper, string assetName)
        {
           
            FontFile = FontLoader.Parse(File.ReadAllText(Path.Combine(helper.ModHelper.DirectoryPath, assetName)));

            CharacterMap = new Dictionary<char, FontChar>();

            foreach (FontChar fontChar in FontFile.Chars)
            {
                char cid = (char)fontChar.ID;
                CharacterMap.Add(cid, fontChar);
            }

            FontPages = new List<Texture2D>();

            foreach (FontPage page in FontFile.Pages)
                FontPages.Add(helper.ModHelper.Content.Load<Texture2D>(Path.Combine(Path.GetDirectoryName(assetName), page.File)));
        }
    }
}
