using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class PlayerShipData : ShipData
    {
        /// <summary>
        /// the multiplier at which a bullet inflicts damage
        /// </summary>
        public float damageFactor;

        public PlayerShipData(GameObject go, string name) : base(go, name)
        {
            damageFactor = 1;
        }
    }
}
