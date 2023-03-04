using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PlatoUI.UI
{
    public interface IDrawHandler
    {
        IEnumerable<Color> Render { get; }
    }
}
