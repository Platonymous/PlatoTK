using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK.Patching
{
    public interface IOnConstruction
    {
        void OnConstruction(IPlatoHelper helper, object linkedObject);
    }
}
