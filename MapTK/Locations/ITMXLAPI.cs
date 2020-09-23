using StardewValley;

namespace MapTK.Locations
{
    public interface ITMXLAPI
    {
        bool TryGetSaveDataForLocation(GameLocation location, out GameLocation saved);
    }
}
