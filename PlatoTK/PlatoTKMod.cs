using StardewModdingAPI;

namespace PlatoTK
{
    public class PlatoTKMod : Mod
    {
        public static PlatoTKMod instance;
        public override void Entry(IModHelper helper)
        {
            instance = this;
        }
    }
}
