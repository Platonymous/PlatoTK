using Microsoft.Xna.Framework;
using StardewValley;

namespace PlatoTK.Events
{
    public interface ICalledEventCommandEventArgs
    {
        Event Event { get; }
        GameLocation Location { get; }

        GameTime Time { get; }

        string Trigger { get; }

        string[] Parameter { get; }
    }

    public interface ICallingEventCommandEventArgs : ICalledEventCommandEventArgs
    {
        void PreventDefault(bool gotoNext = false);
    }
}
