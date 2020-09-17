using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapTK.TileActions
{
    internal class MapTKInventory
    {
        internal const string InventoryDataAsset = @"Data/MapTK/Inventories";


        public string[] Includes { get; set; } = new string[0];

        public List<ShopItem> Inventory { get; set; }

        
        internal static Dictionary<ISalable, int[]> GetInventory(IModHelper helper, string id, string shop)
        {
            return GetPriceAndStock(shop, id, helper);
        }

        private static Dictionary<ISalable, int[]> GetPriceAndStock(string shop, string id, IModHelper helper)
        {
            if (helper.Content.Load<Dictionary<string, MapTKInventory>>(InventoryDataAsset, ContentSource.GameContent).TryGetValue(id, out MapTKInventory inv))
            {
                Dictionary<ISalable, int[]> priceAndStock = new Dictionary<ISalable, int[]>();

                foreach (var item in inv.Inventory)
                    if (GetSalable(item, helper) is ISalable salable)
                        priceAndStock.Add(salable, new int[4] { item.Price == -1 ? salable.salePrice() : item.Price, item.Stock, item.ItemCurrency, item.ItemAmount});

                if (inv.Includes.Length > 0)
                    foreach (var i in inv.Includes)
                        foreach (var include in GetInventory(helper, i, shop))
                            priceAndStock.Add(include.Key, include.Value);

                return priceAndStock;
            }

            return new Dictionary<ISalable, int[]>();
        }
        
        private static ISalable GetSalable(ShopItem shopItem, IModHelper helper)
        {
            Item item = null;

            string type = shopItem.Type;
            int index = shopItem.Index;
            string name = shopItem.Name;

            if (type == "Object")
            {
                if (index != -1)
                    item = new StardewValley.Object(index, 1);
                else if (name != "none")
                    item = new StardewValley.Object(GetIndexByName(Game1.objectInformation, name), 1);
            }
            else if (type == "BigObject")
            {
                if (index != -1)
                    item = new StardewValley.Object(Vector2.Zero, index);
                else if (name != "none")
                    item = new StardewValley.Object(Vector2.Zero, GetIndexByName(Game1.bigCraftablesInformation, name));
            }
            else if (type == "Ring")
            {
                if (index != -1)
                    item = new Ring(index);
                else if (name != "none")
                    item = new Ring(GetIndexByName(Game1.objectInformation, name));
            }
            else if (type == "Hat")
            {
                if (index != -1)
                    item = new Hat(index);
                else if (name != "none")
                    item = new Hat(GetIndexByName(helper.Content.Load<Dictionary<int, string>>(@"Data/hats", ContentSource.GameContent), name));
            }
            else if (type == "Boots")
            {
                if (index != -1)
                    item = new Boots(index);
                else if (name != "none")
                    item = new Boots(GetIndexByName(helper.Content.Load<Dictionary<int, string>>(@"Data/Boots", ContentSource.GameContent), name));
            }
            else if (type == "Clothing")
            {
                if (index != -1)
                    item = new Clothing(index);
                else if (name != "none")
                    item = new Clothing(GetIndexByName(helper.Content.Load<Dictionary<int, string>>(@"Data/ClothingInformation", ContentSource.GameContent), name));
            }
            else if (type == "TV")
            {
                if (index != -1)
                    item = new StardewValley.Objects.TV(index, Vector2.Zero);
                else if (name != "none")
                    item = new TV(GetIndexByName(helper.Content.Load<Dictionary<int, string>>(@"Data/Furniture", ContentSource.GameContent), name), Vector2.Zero);
            }
            else if (type == "IndoorPot")
                item = new StardewValley.Objects.IndoorPot(Vector2.Zero);
            else if (type == "CrabPot")
                item = new StardewValley.Objects.CrabPot(Vector2.Zero);
            else if (type == "Chest")
                item = new StardewValley.Objects.Chest(true);
            else if (type == "Cask")
                item = new StardewValley.Objects.Cask(Vector2.Zero);
            else if (type == "Cask")
                item = new StardewValley.Objects.Cask(Vector2.Zero);
            else if (type == "Furniture")
            {
                if (index != -1)
                    item = new StardewValley.Objects.Furniture(index, Vector2.Zero);
                else if (name != "none")
                    item = new Furniture(GetIndexByName(helper.Content.Load<Dictionary<int, string>>(@"Data/Furniture", ContentSource.GameContent), name), Vector2.Zero);
            }
            else if (type == "Sign")
                item = new StardewValley.Objects.Sign(Vector2.Zero, index);
            else if (type == "Wallpaper")
                item = new StardewValley.Objects.Wallpaper(Math.Abs(index), false);
            else if (type == "Floors")
                item = new StardewValley.Objects.Wallpaper(Math.Abs(index), true);
            else if (type == "MeleeWeapon")
            {
                if (index != -1)
                    item = new MeleeWeapon(index);
                else if (name != "none")
                    item = new MeleeWeapon(GetIndexByName(helper.Content.Load<Dictionary<int, string>>(@"Data/weapons", ContentSource.GameContent), name));

            }
            else if (type == "SDVType")
            {
                if (index == -1)
                    item = Activator.CreateInstance(GetTypeSDV(name)) is Item i ? i : null;
                else
                    item = Activator.CreateInstance(GetTypeSDV(name), index) is Item i ? i : null;

            }
            else if (type == "ByType")
            {
                try
                {
                    if (index == -1)
                        item = Activator.CreateInstance(Type.GetType(name)) is Item i ? i : null;
                    else
                        item = Activator.CreateInstance(Type.GetType(name), index) is Item i ? i : null;
                }
                catch (Exception ex)
                {
                }
            }

            return item;
        }

        private static int GetIndexByName(IDictionary<int, string> dictionary, string name)
        {
            if (name.StartsWith("startswith:"))
                return (dictionary.Where(d => d.Value.Split('/')[0].StartsWith(name.Split(':')[1])).FirstOrDefault()).Key;
            else if (name.StartsWith("endswith:"))
                return (dictionary.Where(d => d.Value.Split('/')[0].EndsWith(name.Split(':')[1])).FirstOrDefault()).Key;
            else if (name.StartsWith("contains:"))
                return (dictionary.Where(d => d.Value.Split('/')[0].Contains(name.Split(':')[1])).FirstOrDefault()).Key;
            else
                return (dictionary.Where(d => d.Value.Split('/')[0] == name).FirstOrDefault()).Key;
        }

        private static Type GetTypeSDV(string type)
        {
            string prefix = "StardewValley.";
            Type defaulSDV = Type.GetType(prefix + type + ", Stardew Valley");

            if (defaulSDV != null)
                return defaulSDV;
            else
                return Type.GetType(prefix + type + ", StardewValley");
        }




    }
}
