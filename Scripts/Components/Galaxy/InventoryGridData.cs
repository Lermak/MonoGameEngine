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
    }
}
