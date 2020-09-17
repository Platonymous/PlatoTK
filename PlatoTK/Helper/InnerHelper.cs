using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoTK
{
    internal abstract class InnerHelper
    {
        protected readonly IPlatoHelper Plato;

        public InnerHelper(IPlatoHelper helper)
        {
            Plato = helper;
        }
    }
}
