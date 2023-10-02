/*
used when fighting i guess
*/
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class PlayerShip : Ship
    {
        public PlayerShip(string texID, string name, Vector2 pos) : base(texID, name, pos)
        {
            this.ComponentHandler.Remove(this.GetComponent("ShipData"));
            ComponentHandler.Add((PlayerShipData) new PlayerShipData(this,"ShipData"));
            AddBehavior("shoot",ShipBehaviors.ShootOnClick);
            AddBehavior("pointToMouse",Behaviors.PointAtMouse);
            //AddBehavior("moveToRot",ShipBehaviors.MoveShipTowardRotation);

            // change this to use shipdata.speed
            AddComponent(new Movement(this,"movement",500,360));
            AddBehavior("wasd",Behaviors.Move);
        }

    }
}
