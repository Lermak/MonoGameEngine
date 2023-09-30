using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class ShipMarker : WorldObject
    {
        public ShipMarker(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "testObject" }, pos, 2)
        {
            AddComponent(new ShipMarkerData(this, "ShipMarkerData"));
            AddBehavior("GoToGalaxy", GalaxyMapBehaviors.TravelOnClick);
            AddBehavior("IdleBob", GalaxyMapBehaviors.ShipMarkerIdleBob);
        }
    }
}
