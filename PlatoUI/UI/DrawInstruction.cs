﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatoUI.UI
{
    public class DrawInstruction : IDrawInstruction
    {
        public Texture2D Texture { get; set; }

        public Rectangle DestinationRectangle { get; set; }

        public Rectangle SourceRectangle { get; set; }

        public Color Color { get; set; }

        public Vector2 Origin { get; set; }

        public float Rotation { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }

        public string Tag { get; }
        
        public DrawInstruction(string tag, Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Tag = tag;
            Texture = texture;
            DestinationRectangle = destinationRectangle;
            if (sourceRectangle.HasValue)
                SourceRectangle = sourceRectangle.Value;
            else
                SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            Color = color;
            Rotation = rotation;
            Origin = origin;
            Effects = effects;
            LayerDepth = layerDepth;
        }

        public virtual void Dispose()
        {
            Texture?.Dispose();
        }
    }
}
