using System.Collections.Generic;

namespace PlatoTK.UI
{
    internal class SpriteFontData
    {
        public int LineSpacing { get; set; }
        public float Spacing { get; set; }
        public char? DefaultCharacter { get; set; }
        public List<char> Characters { get; set; }
        public Dictionary<char, SpriteFontGlyphData> Glyphs { get; set; }
    }
}
