using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.Content
{
    public interface IConditionsProvider
    {
        bool CheckConditions(string conditions, object caller);

        bool TrySubscribeToChange(string conditions, object caller, Action<string, bool> OnChange, out bool state);
    }
}
