using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ShipData : Component
    {
        
        public bool bobDir = false;
        /// <summary>
        /// The ship's onscreen position in fight mode
        /// </summary>
        public Vector2 shipPos;
        
        public ShipData(GameObject go, string name) : base(go, name)
        {
            shipPos = ((WorldObject)go).Transform.Position;

        }
    }
}
