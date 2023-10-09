using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class InventoryGridData : Component
    {
        public const int TILE_SIZE = 96;
        /// <summary>
        /// Horizontal count of items in a grid
        /// </summary>
        public int width;
        /// <summary>
        /// Vertical count of items in a grid
        /// </summary>
        public int height;
        public string[,] cells;
        public List<WorldObject> StoredItems = new List<WorldObject>();

        public Vector2 Size { get { return new Vector2(width * TILE_SIZE, height * TILE_SIZE); } }

        public Vector2 CellZero
        {
            get
            {
                return ((WorldObject)this.gameObject).Transform.Position // center of grid object is center of object
                        + (new Vector2(-width, height) / 2 * TILE_SIZE); //subtract half the size of the grid to get to the top left cell 
            }
        }
        public Vector2 CellMax
        {
            get
            {
                return ((WorldObject)this.gameObject).Transform.Position // center of grid object is center of object
                        + (new Vector2(width, -height) / 2 * TILE_SIZE); //add half the size of the grid to get to the bottom right cell
            }
        }

        public InventoryGridData(GameObject go, string name, int w, int h) : base(go, name)
        {
            width = w;
            height= h;
            cells = new string[width,height];
            for (int x = 0; x < width;  ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    cells[x, y] = "";
                }
            }
        }

        public bool CanPlaceItem(InventoryItem item)
        {
            Vector2 staringCell = GetGridPositionFromWorld(item.PosToGrid);
            for (int i = 0; i < item.ShapeData.GridCells.Length; i++)
            {
                Vector2 pos = staringCell + item.ShapeData.GridCells[i];

                if ( pos.X < 0 || pos.X >= width ) { return false; }
                if ( pos.Y < 0 || pos.Y >= height) { return false; }
                if (cells[(int)pos.X, (int)pos.Y] != "")
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsInsideGrid(InventoryItem item)
        {
            for (int i = 0; i < item.ShapeData.GridCells.Length; i++)
            {
                Vector2 pos = GetGridPositionFromWorld(item.PosToGrid) + item.ShapeData.GridCells[i];

                if (pos.X >= 0 && pos.X <= width &&
                    pos.Y >= 0 && pos.Y <= height) { return true; }
            }
            return false;
        }

        public void PlaceItem(InventoryItem item)
        {
            Vector2 staringCell = GetGridPositionFromWorld(item.PosToGrid);
            for (int i = 0; i < item.ShapeData.GridCells.Length; i++)
            {
                Vector2 pos = staringCell + item.ShapeData.GridCells[i];
                cells[(int)pos.X, (int)pos.Y] = item.Name;
            }
            item.ShapeData.GridPosition = staringCell;
            StoredItems.Add(item);
        }
        public void RemoveItemFromPosition(InventoryItem item)
        {
            for (int i = 0; i < item.ShapeData.GridCells.Length; i++)
            {
                Vector2 pos = GetGridPositionFromWorld(item.PosToGrid) + item.ShapeData.GridCells[i];
                cells[(int)pos.X, (int)pos.Y] = "";
            }
            ((WorldObject) item).RemoveParent();
            item.ShapeData.GridPosition = new Vector2(-1,-1);
            StoredItems.Remove(item);
        }

        public Vector2 GetGridPositionFromWorld(Vector2 worldPos)
        {                                                                 
            Vector2 gridPos = new Vector2((int)(worldPos.X - CellZero.X)/TILE_SIZE, (int)(CellZero.Y - worldPos.Y)/TILE_SIZE);

            return gridPos;
        }
        public Vector2 GetWorldPositionFromGrid(Vector2 grid)
        {
            return CellZero + new Vector2(TILE_SIZE, TILE_SIZE)/2 + grid * TILE_SIZE;
        }

        public bool IsCellEmpty(Vector2 pos)
        {
            return this.cells[(int)pos.X, (int)pos.Y] == "";
        }
        public List<InventoryItem> FetchItemsByType(ItemData.ItemTypes type) {

            List<InventoryItem> typeFilter = new List<InventoryItem>{};
            foreach (WorldObject item in this.StoredItems) {
                // do the casting on add and pray????
                typeFilter.Add(((InventoryItem)item));
            }
            // slice it up
            return typeFilter;

        }
    }
}
