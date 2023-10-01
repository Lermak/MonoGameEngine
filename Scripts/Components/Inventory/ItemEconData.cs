using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class ItemEconData : Component
    {
        public int SalePrice;
        public int PurchasePrice;
        public string DisplayName;
        public string DisplayTexId;

        public ItemEconData(GameObject go, string name) : base(go, name)
        {

        }
    }
}
