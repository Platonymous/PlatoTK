using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatoTK.UI
{
    public interface IDrawInstruction : IDisposable
    {
        string Tag { get; }
        Texture2D Texture { get; set; }
        Rectangle DestinationRectangle { get; }

        Rectangle SourceRectangle { get; }

        Color Color { get; set; }

        Vector2 Origin { get; }

        float Rotation { get; set; }

        SpriteEffects Effects { get; set; }

        float LayerDepth { get; set; }
    }
}
