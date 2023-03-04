using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoUI.UI.Components
{
    public class ParsedData
    {
        public string Attribute { get; }

        public string Value { get; }

        public ParsedData(string attribute, string value)
        {
            Attribute = attribute;
            Value = value;
        }
    }
}
