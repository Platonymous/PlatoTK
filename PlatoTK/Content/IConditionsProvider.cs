using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.Content
{
    public interface IConditionsProvider
    {
        string Id { get; }
        bool CanHandleConditions(string conditions);
        bool CheckConditions(string conditions, object caller);
    }
}
