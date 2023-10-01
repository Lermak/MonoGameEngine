using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class EnemyShip : WorldObject {
    public EnemyShip(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "combat","enemy" }, pos, 2)
        {
            AddComponent(new EnemyShipData(this, "enemyShipData"));
            AddBehavior("moveToRot",Behaviors.MoveTowardRotation);
        }
}