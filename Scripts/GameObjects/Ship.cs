/*
used when fighting i guess
*/
using System;
using System.Collections.Generic;
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
            AddComponent(new RigidBody(this,RigidBody.RigidBodyType.Dynamic));
        }
    }
}
