using StardewValley;

namespace MapTK.TileActions
{
    internal class BoughtItem
    {
        public string ItemName { get; set; }

        public int Stock { get; set; }

        public BoughtItem()
        {

        }

        public BoughtItem(ISalable item, int stock)
        {
            ItemName = item.Name;
            Stock = stock;
        }
    }
}
