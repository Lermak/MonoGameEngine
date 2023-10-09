using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class EnemyShip : WorldObject {
    public EnemyShip(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "combat","enemy" }, pos, 2)
        {
            //ComponentHandler.Remove(GetComponent("ShipData"));
            AddComponent(new EnemyShipData(this, "enemyShipData"));
            AddComponent(new CollisionBox(this,"enemyBox",false,ResourceManager.GetTextureSize(texID)));
            AddBehavior("moveToRot",Behaviors.MoveTowardRotation);
            AddBehavior("die",ShipBehaviors.DieWhenDead);
        }
}