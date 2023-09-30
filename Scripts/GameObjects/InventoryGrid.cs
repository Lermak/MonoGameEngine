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
            if (slots[(int)item.gridPos.X, (int)item.gridPos.Y] == true) { return false; }
            for (int i = 0; i < 3; i++)
            {
                if (slots[(int)(item.gridPos.X + item.blocks[i].X), (int)(item.gridPos.Y + +item.blocks[i].Y)] == true) { return false; }
            }
            return true;
        }

        public void placeItem(InventoryItem item)
        {
            if (canPlaceItem(item)) {
                slots[(int)item.gridPos.X, (int)item.gridPos.Y] = true;
                for (int i = 0; i < 3; i++)
                {
                    slots[(int)(item.gridPos.X + item.blocks[i].X), (int)(item.gridPos.Y + +item.blocks[i].Y)] = true;
                }
            }
        }

        public InventoryItem grabItem(Vector2 position)
        {
            return null;
        }

        public List<InventoryItem> items;
        public bool[,] slots = null;
        public InventoryGrid(string texID, string name) : base(texID, name, new string[] { "InventoryGrid" }, new Vector2(0, 0), 1)
        {
            this.AddBehavior("MarkSlotOnClick", Behaviors.MarkSlotOnClick);
            this.slots = new bool[width, height];
            items = new List<InventoryItem>();
        }

        
    }
}