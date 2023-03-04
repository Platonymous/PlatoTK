using StardewModdingAPI;
using System.Collections.Generic;
using System.Linq;

namespace PlatoUI
{
    public static class HelperExtension
    {
        public static HashSet<IPlatoUIHelper> Helper = new HashSet<IPlatoUIHelper>();

        public static IPlatoUIHelper GetPlatoUIHelper(this IModHelper helper)
        {
            IPlatoUIHelper platoHelper = Helper.FirstOrDefault(p => p.ModHelper.ModRegistry.ModID == helper.ModRegistry.ModID);
            if (platoHelper is IPlatoUIHelper)
                return platoHelper;

            platoHelper = new PlatoHelper(helper);
            Helper.Add(platoHelper);

            return platoHelper;
        }

        public static IPlatoUIHelper GetPlatoHelper(this Mod mod) => mod.Helper.GetPlatoUIHelper();
    }
}
