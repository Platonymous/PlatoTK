using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatoTK.Patching
{
    public interface ITextureDrawHandler
    {
        string Id { get; }

        SpriteBatch SpriteBatch { get; }
        Texture2D Texture { get; set; }

        Rectangle DestinationRectangle { get; set; }

        Rectangle? SourceRectangle { get; set; }

        Color Color { get; set; }

        Vector2 Origin { get; set; }

        float Rotation { get; set; }

        SpriteEffects Effects { get; set; }

        float LayerDepth { get; set; }
        void Draw();
    }

    public interface ITextureDrawHandler<TData> : ITextureDrawHandler
    {
        TData Data { get; }

    }
}
