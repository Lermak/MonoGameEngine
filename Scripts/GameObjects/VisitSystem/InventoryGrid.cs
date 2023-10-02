using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class InventoryGrid : WorldObject
    {
        public static InventoryGrid Grid = null;
        public static InventoryGridData Inventory { get { return (InventoryGridData)Grid.componentHandler.Get("Grid"); } }

        public InventoryGrid(string texID, string name, Vector2 gridSize) : base(texID, name, new string[] { "InventoryGrid" }, new Vector2(0, 0), 1)
        {
            if (Grid == null)
            {
                Grid = this;
                AddComponent(new InventoryGridData(this, "Grid", (int)gridSize.X, (int)gridSize.Y));
            }
        }
        
    }
}