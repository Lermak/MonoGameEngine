using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class InventoryGrid : WorldObject
    {
        public int width = 19;
        public int height = 10;
        public bool isCellEmpty()
        {
            return false;
        }

        public bool canPlaceItem(InventoryItem item)
        {
            //for each square of the item, see if position in grid is free
            Vector2 pos = ((Transform)item.GetComponent("transform")).Position;

            for (int i = 0; i < 4; i++)
            {
                WorldObject obj = (WorldObject)SceneManager.CurrentScene.GetObjectByName(item.blocks[i]);
                if (slots[(int)(pos.X + obj.Transform.Position.X), (int)(pos.Y + obj.Transform.Position.Y)] == item.blocks[i]) { return false; }
            }
            return true;
        }

        public void placeItem(InventoryItem item)
        {
            Vector2 pos = ((Transform)item.GetComponent("transform")).Position;
            if (canPlaceItem(item)) {
                for (int i = 0; i < 4; i++)
                {
                    WorldObject obj = (WorldObject)SceneManager.CurrentScene.GetObjectByName(item.blocks[i]);
                    slots[(int)(pos.X + obj.Transform.Position.X), (int)(pos.Y + obj.Transform.Position.Y)] = item.blocks[i];
                }
            }
            this.items.Add(item);
        }

        public InventoryItem grabItem(Vector2 position)
        {
            return null;
        }

        public List<InventoryItem> items;
        public string[,] slots = null;
        public InventoryGrid(string texID, string name) : base(texID, name, new string[] { "InventoryGrid" }, new Vector2(0, 0), 1)
        {
            this.AddBehavior("MarkSlotOnClick", Behaviors.MarkSlotOnClick);
            this.slots = new string[width, height];
            items = new List<InventoryItem>();
        }

        
    }
}