﻿using System.Collections.Generic;

namespace MapTK.TileActions
{
    internal class ShopInventoryToken
    {
        public bool IsMutable() => false;
        public bool AllowsInput() => false;
        public bool RequiresInput() => false;
        public bool CanHaveMultipleValues(string input = null) => false;
        public bool UpdateContext() => false;
        public bool IsReady() => true;

        public virtual IEnumerable<string> GetValues(string input)
        {
            return new[] { MapTKInventory.InventoryDataAsset };
        }
    }
}
