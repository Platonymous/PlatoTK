using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Tiles;

namespace MapTK.Api
{
    public class MapTKAPI
    {
        internal static readonly HashSet<Func<Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float, bool>> TileDrawHandlers = new HashSet<Func<Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float, bool>>();

        public void HandleTileDraws(Func<Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float,bool> handler)
        {
            TileDrawHandlers.Add(handler);
        }
    }
}
