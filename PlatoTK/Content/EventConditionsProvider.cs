using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.Content
{
    internal class EventConditionsProvider : IConditionsProvider
    {
        public bool CheckConditions(string conditions, object caller)
        {
            GameLocation location = caller is GameLocation gl ? gl : Game1.currentLocation;

            if (location == null)
                return false;

            var m = typeof(GameLocation).GetMethod("checkEventPrecondition", BindingFlags.NonPublic | BindingFlags.Instance);
            return (int)m.Invoke(location, new string[] { ("9999999/" + conditions) }) > 0;
        }

        public bool TrySubscribeToChange(string conditions, object caller, Action<string, bool> OnChange, out bool state)
        {
            state = CheckConditions(conditions, caller);
            return false;
        }
    }
}
