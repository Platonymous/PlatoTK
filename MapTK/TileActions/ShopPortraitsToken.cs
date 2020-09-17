using System.Collections.Generic;
using System.IO;

namespace MapTK.TileActions
{
    internal class ShopPortraitsToken
    {
        internal const string ShopPortraitsPrefix = @"MapTK/ShopPortraits/";
        public bool IsMutable() => false;
        public bool AllowsInput() => false;
        public bool RequiresInput() => false;
        public bool CanHaveMultipleValues(string input = null) => false;
        public bool UpdateContext() => false;
        public bool IsReady() => true;

        public virtual IEnumerable<string> GetValues(string input)
        {
            return new[] { Path.Combine(ShopPortraitsPrefix, input) };
        }
    }
}
