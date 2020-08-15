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

        public void SetDelayedUpdateAction(int delay, Action action)
        {
            SetTickHandler(action, delay, false);
        }

        public void SetTickDelayedUpdateAction(int delay, Action action)
        {
            SetTickHandler(action, delay, true);
        }

        private void SetTickHandler(Action action, int delay, bool ticks)
        {
            long target = !ticks ? Game1.currentGameTime.TotalGameTime.Milliseconds + delay : delay;

            EventHandler<StardewModdingAPI.Events.UpdateTickingEventArgs> tickHandler = null;
            tickHandler = (sender, e) =>
            {
                if (ticks)
                    target--;

                if (!ticks && Game1.currentGameTime.TotalGameTime.Milliseconds >= target)
                    target = 0;

                if (target <= 0)
                {
                    ModHelper.Events.GameLoop.UpdateTicking -= tickHandler;
                    action();
                }
            };

            ModHelper.Events.GameLoop.UpdateTicking += tickHandler;
        }
    }
}
