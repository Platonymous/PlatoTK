using StardewModdingAPI;
using System.Collections.Generic;
using System.Linq;

namespace PlatoTK
{
    public static class HelperExtension
    {
        public static HashSet<IPlatoHelper> Helper = new HashSet<IPlatoHelper>();

        public static IPlatoHelper GetPlatoHelper(this IModHelper helper)
        {
            IPlatoHelper platoHelper = Helper.FirstOrDefault(p => p.ModHelper.ModRegistry.ModID == helper.ModRegistry.ModID);
            if (platoHelper is IPlatoHelper)
                return platoHelper;

            platoHelper = new PlatoHelper(helper);
            Helper.Add(platoHelper);

            return platoHelper;
        }

        public static IPlatoHelper GetPlatoHelper(this Mod mod) => mod.Helper.GetPlatoHelper();
    }
}
