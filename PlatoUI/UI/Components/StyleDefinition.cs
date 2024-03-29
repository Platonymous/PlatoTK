﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatoUI.UI.Components
{
    public class StyleDefinition
    {
        public string Key { get; }
        public string Value { get; }
        public string Option { get; } = "";

        public StyleDefinition(string key, string value)
        {
            Value = value;

            if (!key.Contains(':'))
            {
                Key = key;
                return;
            }

            string[] keySplit = key.Split(':');
            Key = keySplit[0];
            Option = keySplit[1];
        }
    }
}
