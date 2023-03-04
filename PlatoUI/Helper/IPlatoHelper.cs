using PlatoUI.Content;
using PlatoUI.UI;
using StardewValley;
using System;

namespace PlatoUI
{
    public interface IPlatoUIHelper
    {  
        IUIHelper UI { get; }
        IContentHelper Content { get; }

        StardewModdingAPI.IModHelper ModHelper { get; }
        DelayedAction SetDelayedAction(int delay, Action action);
        void SetDelayedUpdateAction(int delay, Action action);
        void SetTickDelayedUpdateAction(int delay, Action action);
    }
}
