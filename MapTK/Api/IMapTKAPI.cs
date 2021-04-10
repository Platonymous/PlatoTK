using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MapTK.Api
{
    public interface IMapTKAPI
    {
        void HandleTileDraws(Func<Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float, bool> handler);
    }
}
