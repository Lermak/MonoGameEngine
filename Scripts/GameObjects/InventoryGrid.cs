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
                Vector2 gridPos = worldToGrid(obj.Transform.Position);
                if (slots[(int)(gridPos.X), (int)( gridPos.Y)] == item.blocks[i]) { return false; }
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
                    Vector2 gridPos = worldToGrid(obj.Transform.Position);
                    slots[(int)(gridPos.X), (int)(gridPos.Y)] = item.blocks[i];
                }
            }
            this.items.Add(item);
        }

        public static Vector2 worldToGrid(Vector2 world)
        {
            float gridX = (((world.X / (Globals.SCREEN_WIDTH - 100)) + 0.5f) * Globals.inventoryGrid.width);
            float gridY = (((world.Y / (Globals.SCREEN_HEIGHT - 100)) + 0.5f) * Globals.inventoryGrid.height);
            gridX = Math.Clamp(gridX, 0, Globals.inventoryGrid.width - 1);
            gridY = Math.Clamp(gridY, 0, Globals.inventoryGrid.height - 1);
            return new Vector2(gridX, gridY);
        }
        public static Vector2 gridToWorld(Vector2 grid)
        {
            float worldX = ((grid.X / Globals.inventoryGrid.width) - 0.5f) * (Globals.SCREEN_WIDTH - 200);
            float worldY = ((grid.Y / Globals.inventoryGrid.height) - 0.5f) * (Globals.SCREEN_HEIGHT - 200);
            return new Vector2(worldX, worldY);
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