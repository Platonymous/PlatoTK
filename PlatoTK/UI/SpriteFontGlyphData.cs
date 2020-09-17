using Microsoft.Xna.Framework;

namespace PlatoTK.UI
{
    internal class SpriteFontGlyphData
    {
        public Rectangle BoundsInTexture { get; set; }
        public Rectangle Cropping { get; set; }
        public char Character { get; set; }

        public float LeftSideBearing { get; set; }
        public float Width { get; set; }
        public float RightSideBearing { get; set; }
    }
}
