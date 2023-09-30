using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public static class GalaxyMapCoroutines
    {
        public static IEnumerator<bool> TravelToGalaxy(WorldObject ship, Transform galaxy)
        {
            Vector2 mv = (galaxy.Position - ship.Transform.Position);
            mv.Normalize();
            ship.RigidBody.MoveVelocity = mv * 3;
            GalaxyData gd = ((GalaxyData)galaxy.GameObject.GetComponent("GalaxyData"));
            ShipMarkerData sd = (ShipMarkerData)ship.GetComponent("ShipMarkerData");

            while (Math.Abs(Vector2.Distance(ship.Transform.Position, galaxy.Position)) > ((CollisionCircle)(galaxy.GameObject.GetComponent("myCircle"))).Hypotenuse/2)
            {
                yield return false;
            }
            ship.RigidBody.MoveVelocity = new Vector2();
            sd.InFlight = false;
            sd.Row = gd.Row;
            sd.idePos = ship.Transform.Position;
            gd.GoShopping = true;
            yield return true;
        }
    }
}
