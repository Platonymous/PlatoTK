using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatoTK.Patching
{
    public interface IPlatoTexture
    {
        string Id { get; }
        bool SkipHandler { get; }
        bool CallTextureHandler(SpriteBatch spriteBatch, Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, Vector2 origin, float rotation, SpriteEffects effects, float layerDepth);
    }

    public interface IPlatoTexture<TData> : IPlatoTexture
    {
        TData Data { get; set; }
    }
}
