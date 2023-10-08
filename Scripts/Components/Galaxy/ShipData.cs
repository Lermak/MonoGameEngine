using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ShipData : Component
    {
        
        
        /// <summary>
        /// Object's speed relative to x percent of MoveVelocity
        /// </summary>
        public float speed;
        

        public ShipData(GameObject go, string name) : base(go, name)
        {
            speed = 250;
            
        }
    }
}
