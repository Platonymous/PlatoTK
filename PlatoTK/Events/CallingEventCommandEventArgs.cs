using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Linq;

namespace PlatoTK.Events
{
    internal class CallingEventCommandEventArgs : ICallingEventCommandEventArgs, ICalledEventCommandEventArgs
    {
        public Event Event { get; }

        private readonly string[] Commands;

        private readonly Action Callback;

        public GameLocation Location { get; }

        public GameTime Time { get; }

        public string Trigger => Commands.Length > 0 ? Commands[0] : "";

        public string[] Parameter => Commands.Length > 1 ? Commands.Skip(1).ToArray() : new string[0];

        public CallingEventCommandEventArgs(string[] commands, Event eventInstance, GameTime time, GameLocation location, Action callback){
            Commands = commands;
            Event = eventInstance;
            Time = time;
            Location = location;
            Callback = callback;
        }

        public void PreventDefault(bool next = false)
        {
            ++Event.CurrentCommand;
            if (next)
                Event.checkForNextCommand(Location, Time);
            Callback?.Invoke();
        }
    }
}
