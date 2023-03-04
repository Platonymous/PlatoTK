using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoUI
{
    internal abstract class InnerHelper
    {
        protected readonly IPlatoUIHelper Plato;

        public InnerHelper(IPlatoUIHelper helper)
        {
            Plato = helper;
        }
    }
}
