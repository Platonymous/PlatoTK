using StardewModdingAPI;

namespace PlatoUI
{
    public class PlatoUIMod : Mod
    {
        public override void Entry(IModHelper helper)
        {
            var plato = helper.GetPlatoUIHelper();
        }
    }
}
