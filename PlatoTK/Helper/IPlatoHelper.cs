using PlatoTK.Content;
using PlatoTK.Patching;
using PlatoTK.Network;
using PlatoTK.UI;
using StardewValley;
using System;
using PlatoTK.Events;
using PlatoTK.Lua;

namespace PlatoTK
{
    public interface IPlatoHelper
    {
        ISharedDataHelper SharedData { get; }
        IHarmonyHelper Harmony { get; }
        IContentHelper Content { get; }
        ILuaHelper Lua { get; }

        IPlatoEventsHelper Events { get; }

        IUIHelper UI { get; }
        StardewModdingAPI.IModHelper ModHelper { get; }
        DelayedAction SetDelayedAction(int delay, Action action);

        void SetDelayedUpdateAction(int delay, Action action);
        void SetTickDelayedUpdateAction(int delay, Action action);
        bool CheckConditions(string conditions, object caller);

        void AddConditionsProvider(IConditionsProvider provider);
    }
}
