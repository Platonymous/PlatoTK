namespace MapTK.TileActions
{
    internal class ShopItem
    {
        public int Index { get; set; } = -1;

        public string Name { get; set; } = "none";

        public string Type { get; set; } = "Object";

        public int Stock { get; set; } = int.MaxValue;

        public int Price { get; set; } = -1;

        public int ItemCurrency { get; set; } = -1;

        public int ItemAmount { get; set; } = 5;
    }
}
