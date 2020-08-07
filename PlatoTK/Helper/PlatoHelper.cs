using PlatoTK.Content;
using PlatoTK.Harmony;
using PlatoTK.Network;
using PlatoTK.UI;
using StardewValley;
using System;

namespace PlatoTK
{
    internal class PlatoHelper : IPlatoHelper
    {
        public ISharedDataHelper SharedData { get; }
        public IHarmonyHelper Harmony { get; }

        public IContentHelper Content { get; }

        public StardewModdingAPI.IModHelper ModHelper { get; }

        public IUIHelper UI { get; }

        public PlatoHelper(StardewModdingAPI.IModHelper helper)
        {
            ModHelper = helper;
            SharedData = new SharedDataHelper(this);
            Harmony = new HarmonyHelper(this);
            Content = new ContentHelper(this);
            UI = new UIHelper(this);
        }

        public DelayedAction SetDelayedAction(int delay, Action action)
        {
            DelayedAction d = new DelayedAction(delay, () => action());
            Game1.delayedActions.Add(d);
            return d;
        }
    }
}
