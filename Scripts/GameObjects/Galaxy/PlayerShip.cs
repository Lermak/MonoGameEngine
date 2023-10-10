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
            ShipData data = (PlayerShipData) new PlayerShipData(this,"ShipData");
            ComponentHandler.Add(data);
            AddBehavior("pointToMouse",Behaviors.PointAtMouse);
            AddComponent(new Movement(this,"movement",data.speed,0));
            AddBehavior("wasd",Behaviors.MoveWithRot);
        }
    }
}
