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
    public class Ship : WorldObject
    {
        public Ship(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "fighting" }, pos, 2)
        {
            AddComponent(new ShipData(this, "ShipData"));
            //AddComponent(new RigidBody(this,RigidBody.RigidBodyType.Dynamic));
            AddBehavior("shoot",ShipBehaviors.ShootOnClick);
            AddBehavior("pointToMouse",Behaviors.PointAtMouse);
            AddBehavior("moveToRot",Behaviors.MoveTowardRotation);
        }
        /// <summary>
        ///  checks if there are no spaces filled in the ship inventory grid
        /// </summary>
        /// <param name="inv"></param>
        /// <returns></returns>
        public bool IsDead(string[] inv) {
            List<string> filled = new List<string> {};

            foreach (string item in inv) {
                if (!(item == "")) {
                    filled.Add(item);
                }
            }
            if (filled.Count > 0) {
                // if theres somethign in there ship is alive
                return false;
            } else {
                // if its empty gg
                return true;
            }
            
        }

    }
}
