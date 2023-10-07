using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ShipData : Component
    {
        
        /// <summary>
        /// Count of bullet reserves for the ship's energy weapons
        /// 
        /// corbin thought it would be funny to make this a float
        /// </summary>
        public float reload;
        public float reloadSpeed;
        /// <summary>
        /// Object's speed relative to x percent of MoveVelocity
        /// </summary>
        public float speed;
        

        public ShipData(GameObject go, string name) : base(go, name)
        {
            reload = 100;
            speed = 250;
            reloadSpeed = 90;
        }
    }
}
