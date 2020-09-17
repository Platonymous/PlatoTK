using PlatoTK;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace MapTK.Locations
{
    internal class LocationsHandler
    {
        const string LocationSaveData = @"MapTK.SaveData.Locations";
        internal const string LocationsDictionary = @"MapTK/Locations";
        readonly Type[] ExtraTypes = new Type[24]
        {
            typeof(Tool),
            typeof(Duggy),
            typeof(Ghost),
            typeof(GreenSlime),
            typeof(LavaCrab),
            typeof(RockCrab),
            typeof(ShadowGuy),
            typeof(Child),
            typeof(Pet),
            typeof(Dog),
            typeof(Cat),
            typeof(Horse),
            typeof(SquidKid),
            typeof(Grub),
            typeof(Fly),
            typeof(DustSpirit),
            typeof(Bug),
            typeof(BigSlime),
            typeof(BreakableContainer),
            typeof(MetalHead),
            typeof(ShadowGirl),
            typeof(Monster),
            typeof(JunimoHarvester),
            typeof(TerrainFeature)
        };

        readonly XmlWriterSettings SaveWriterSettings = new XmlWriterSettings()
        {
            ConformanceLevel = ConformanceLevel.Auto,
            CloseOutput = true
        };

        readonly XmlReaderSettings SaveReaderSettings = new XmlReaderSettings()
        {
            ConformanceLevel = ConformanceLevel.Auto,
            CloseInput = true
        };

        readonly IModHelper Helper;

        public LocationsHandler(IModHelper helper)
        {
            Helper = helper;

            helper.GetPlatoHelper().Content.Injections.InjectLoad(LocationsDictionary, new Dictionary<string, LocationData>());


            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
            helper.Events.GameLoop.SaveLoaded += InitializeNewLocations;
            helper.Events.GameLoop.SaveCreated += InitializeNewLocations;
            helper.Events.GameLoop.Saving += GameLoop_Saving;
        }

        private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var api = Helper.ModRegistry.GetApi<PlatoTK.APIs.IContentPatcher>("Pathoschild.ContentPatcher");
            api.RegisterToken(Helper.ModRegistry.Get(Helper.ModRegistry.ModID).Manifest, "Locations", new LocationsToken());
        }

        private void InitializeNewLocations(object sender, EventArgs e)
        {
            Helper.Content.Load<Dictionary<string,LocationData>>(LocationsDictionary,ContentSource.GameContent)
                .Where(l => !Game1.locations.Any(g => g.Name == l.Value.Name))
                .ToList()
                .ForEach(l => Game1.locations.Add(GetNewLocation(l.Value)));
        }

        private GameLocation GetNewLocation(LocationData data)
        {
            string type = data.Type.ToLower();

            if(data.Options.Contains("save"))
            try
            {
                if (Helper.Data.ReadSaveData<LocationSaveData>($"{LocationSaveData}") is LocationSaveData saveDataStore 
                    && saveDataStore.Locations.ContainsKey(data.Name) && saveDataStore.Locations[data.Name] is string savedata
                    && !string.IsNullOrEmpty(savedata))
                {
                    XmlSerializer serializer = null;

                    if (Type.GetType(data.Type) is Type customType)
                        serializer = new XmlSerializer(customType, ExtraTypes);
                    else
                        serializer = new XmlSerializer(typeof(GameLocation), ExtraTypes);

                    using (StringReader dataReader = new StringReader(savedata))
                    using (var reader = XmlReader.Create(dataReader,SaveReaderSettings))
                        if (serializer.Deserialize(reader) is GameLocation savedLocation)
                            return savedLocation;
                }
            }catch
            {

            }

            GameLocation result;

            switch (type)
            {
                case "buildable": result = new BuildableGameLocation(data.MapPath, data.Name);break;
                case "decoratable": result = new DecoratableLocation(data.MapPath, data.Name);break;
                case "default": result = new GameLocation(data.MapPath, data.Name);break;
                default:
                    {
                        if (Type.GetType(data.Type) is Type customType
                            && Activator.CreateInstance(customType, data.MapPath, data.Name) is GameLocation customLocation)
                            result = customLocation;
                        else
                            result = new GameLocation(data.MapPath, data.Name);
                        break;
                    }
            }

            if (data.Options.Contains("farm"))
                result?.isFarm.Set(true);

            if (data.Options.Contains("greenhouse"))
                result?.isGreenhouse.Set(true);
            
            return result;
        }

        private void GameLoop_Saving(object sender, SavingEventArgs e)
        {
            var locationDataStore = new LocationSaveData();

            Helper.Content.Load<Dictionary<string, LocationData>>(LocationsDictionary, ContentSource.GameContent)
                .Where(l => l.Value.Options.Any(o=> o.ToLower() == "save") && Game1.getLocationFromName(l.Value.Name) is GameLocation)
                .Select(l => Game1.getLocationFromName(l.Value.Name))
                .ToList()
                .ForEach((location) =>
                {
                    try
                    {
                        using (StringWriter dataWriter = new StringWriter())
                        using (var writer = XmlWriter.Create(dataWriter, SaveWriterSettings))
                        {
                            XmlSerializer serializer = new XmlSerializer(location.GetType(), ExtraTypes);
                            serializer.Serialize(writer, location);
                            string savedata = dataWriter.ToString();
                            
                            locationDataStore.Locations.Add(location.Name, savedata);
                        }
                    }
                    catch
                    {

                    }
                });

            Helper.Data.WriteSaveData($"{LocationSaveData}", locationDataStore);
        }
    }
}
