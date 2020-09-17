using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Linq;
using xTile;
using xTile.Layers;
using xTile.Tiles;

namespace PlatoTK.Events
{
    internal class CallingTileActionEventArgs : ICallingTileActionEventArgs
    {
        private readonly string[] Commands;

        private readonly Action<bool> Callback;

        private Tile _tile;

        public string FullString => string.Join(" ", Commands);

        public string[] Parameter => Commands.Skip(1).ToArray();

        public string Trigger => Commands[0];

        public Farmer Caller { get; }

        public GameLocation Location { get; }

        public Point Position { get; }

        public Tile Tile { 
            get {
                if (_tile != null)
                    return _tile;

                _tile = Map?.Layers
                    .Select(l => l.Tiles[Position.X, Position.Y])
                    .FirstOrDefault(t => t != null && t.Properties.Any(p => p.Value != null && p.ToString().Contains(FullString)));

                return _tile;
            }
        }
        public Layer Layer => Tile?.Layer;

        public Map Map => Location?.Map;

        public CallingTileActionEventArgs(string[] commands, Farmer who, GameLocation location, Point position, Action<bool> callback)
        {
            Commands = commands;
            Caller = who ?? Game1.player;
            Location = location ?? Game1.currentLocation;
            Position = position == null ? Point.Zero : position;
            Callback = callback;
        }

        public void TakeOver(bool preventDefault)
        {
            Callback?.Invoke(preventDefault);
        }
    }

}
