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

        public string DisplayName;

        public ItemTypes ItemType;

        public ItemData(GameObject go, string name, string display, ItemTypes t) : base(go, name)
        {
            DisplayName = display;
            ItemType = t;
        }

        public static ItemData Random(GameObject go)
        {
            Random r = new Random();

            ItemTypes it = (ItemTypes)r.Next(Enum.GetNames(typeof(ItemTypes)).Length - 1);
            string display = it.ToString();
            //TODO if item is combat type also have go.addComponent(combatData)
            return new ItemData(go, "ItemData", display, it);
            

        }
    }
}
