using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class ItemData : Component
    {
        public enum ItemTypes
        {
            Combat,
            Tool,
            Resource,
            Economy
        }

        public int SellPrice;
        public int PurchasePrice;
        public string DisplayName;

        public ItemTypes ItemType;
        public bool Sell;

        public ItemData(GameObject go, string name, int sell, int purchase, string display, ItemTypes t) : base(go, name)
        {
            Sell = false;
            SellPrice = sell;
            PurchasePrice = purchase;
            DisplayName = display;
            ItemType = t;
        }

        public static ItemData Random(GameObject go)
        {
            Random r = new Random();

            int sell = r.Next(1, 10);
            int purchase = r.Next(1, 4);
            ItemTypes it = (ItemTypes)r.Next(Enum.GetNames(typeof(ItemTypes)).Length - 1);
            string display = it.ToString();

            return new ItemData(go, "ItemData", sell, purchase, display, it);
            

        }
    }
}
