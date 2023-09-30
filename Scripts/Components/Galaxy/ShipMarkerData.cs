using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ShipMarkerData : Component
    {
        public bool InFlight = false;
        public int Row = -1;
        public bool bobDir = false;
        public Vector2 idePos;
        public ShipMarkerData(GameObject go, string name) : base(go, name)
        {
            idePos = ((WorldObject)go).Transform.Position;
        }
    }
}
