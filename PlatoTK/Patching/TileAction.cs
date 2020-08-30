using System;

namespace PlatoTK.Patching
{
    internal class TileAction : ITileAction
    {
        public string[] Trigger { get; }

        public Action<ITileActionTrigger> Handler { get; }

        public TileAction(Action<ITileActionTrigger> handler, params string[] trigger)
        {
            Trigger = trigger;
            Handler = handler;
        }
    }
}
