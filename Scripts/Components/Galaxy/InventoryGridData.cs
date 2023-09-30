using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class InventoryGridData : Component
    {
        
        // todo: scaff/actually put stuff here
        /// <summary>
        /// Horizontal count of items in a grid
        /// </summary>
        public int width;
        /// <summary>
        /// Vertical count of items in a grid
        /// </summary>
        public int height;
        public string[,] cells;

        public InventoryGridData(GameObject go, string name, int w, int h) : base(go, name)
        {

            width = w;
            height= h;
            cells = new string[width,height];
        }

        public bool canPlaceItem(InventoryItemData item)
        {
            for (int i = 0; i < 4; i++)
            {
                int x = item.position.X + item.offset[i].X;
                int y = item.position.Y + item.offset[i].Y;
                if ( x < 0 || x > width ) { return false; }
                if ( y < 0 || y > height) { return false; }
                if (cells[x,y] != "")
                {
                    return false;
                }
            }
            return true;
        }
        public void placeItem(InventoryItemData item)
        {
            for (int i = 0; i < 4; i++)
            {
                int x = item.position.X + item.offset[i].X;
                int y = item.position.Y + item.offset[i].Y;
                cells[x, y] = item.Name;
            }
            this.GameObject.AddComponent(item);
        }

        public InventoryItemData removeItemFromPosition((int X, int Y) gridPosition)
        {
            InventoryItemData item = (InventoryItemData)this.gameObject.GetComponent(this.cells[gridPosition.X, gridPosition.Y]);
            this.gameObject.ComponentHandler.Components.Remove(item);
            for (int i = 0; i < 4; i++)
            {
                int x = item.position.X + item.offset[i].X;
                int y = item.position.Y + item.offset[i].Y;
                cells[x, y] = "";
            }
            return item;
        }

        public (int X, int Y) getGridPositionFromWorld(Vector2 worldPos)
        {
            float gridX = (((worldPos.X / (Globals.SCREEN_WIDTH - 100)) + 0.5f) * Globals.inventoryGrid.width);
            float gridY = (((worldPos.Y / (Globals.SCREEN_HEIGHT - 100)) + 0.5f) * Globals.inventoryGrid.height);
            gridX = Math.Clamp(gridX, 0, Globals.inventoryGrid.width - 1);
            gridY = Math.Clamp(gridY, 0, Globals.inventoryGrid.height - 1);
            return ((int)gridX, (int)gridY);
        }
        public Vector2 getWorldPositionFromGrid( (int X, int Y) grid )
        {
            float worldX = (((grid.X / Globals.inventoryGrid.width) - 0.5f) * (Globals.SCREEN_WIDTH - 100)) + (Globals.TILE_SIZE / 2);
            float worldY = (((grid.Y / Globals.inventoryGrid.height) - 0.5f) * (Globals.SCREEN_HEIGHT - 100)) + (Globals.TILE_SIZE / 2);
            return new Vector2(worldX, worldY);
        }
    }
}
