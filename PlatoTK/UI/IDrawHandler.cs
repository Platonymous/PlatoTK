using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PlatoTK.UI
{
    public interface IDrawHandler
    {
        IEnumerable<Color> Render { get; }
    }
}
