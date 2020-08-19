using PlatoTK.UI.Components;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.UI
{
    public interface IUIMenu
    {
        Func<bool> ShouldDraw { get; set; }
        IWrapper Menu { get; }
    }
}
