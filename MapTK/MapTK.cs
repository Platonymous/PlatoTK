using StardewModdingAPI;
using MapTK.SpouseRooms;
using MapTK.Locations;
using MapTK.MapExtras;
using StardewValley;
using System;
using MapTK.FestivalSpots;
using System.Linq;
using MapTK.TileActions;
using PlatoTK;

namespace MapTK
{
    public class MapTK : Mod
    {
        internal static LocationsHandler LocationsHandler;
        internal static ExtraLayersHandler ExtraLayersHandler;
        internal static SpouseRoomHandler SpouseRoomHandler;
        internal static FestivalSpotsHandler FestivalSpotsHandler;
        internal static MapTKDisplayDevice MapDisplayDevice;

        public override void Entry(IModHelper helper)
        {
            LocationsHandler = new LocationsHandler(helper);
            ExtraLayersHandler = new ExtraLayersHandler(helper);
            SpouseRoomHandler = new SpouseRoomHandler(helper);
            FestivalSpotsHandler = new FestivalSpotsHandler(helper);
            helper.Events.GameLoop.DayStarted += SetMapDisplayDevice;
            helper.Events.GameLoop.SaveLoaded += SetMapDisplayDevice;
            helper.Events.GameLoop.SaveCreated += SetMapDisplayDevice;
            helper.Events.Player.Warped += SetMapDisplayDevice;
            helper.Events.GameLoop.GameLaunched += SetMapDisplayDevice;
            helper.Events.GameLoop.ReturnedToTitle += SetMapDisplayDevice;
            helper.Content.AssetLoaders.Add(new GameAssetLoader(helper));
        }

        private void SetMapDisplayDevice(object sender, EventArgs e)
        {
            if (MapDisplayDevice == null)
                MapDisplayDevice = new MapTKDisplayDevice(Game1.content, Game1.graphics.GraphicsDevice, Helper.ModRegistry.IsLoaded("DigitalCarbide.SpriteMaster"));

            Game1.mapDisplayDevice = MapDisplayDevice;
        }
    }
}
