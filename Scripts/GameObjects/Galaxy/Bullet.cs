using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class Bullet : WorldObject {
    public BulletData Data {
        get {return (BulletData)GetComponent("BulletData");}
    }
    public Bullet(string texID,string name,Vector2 pos,float parentRot)
    : base(texID,name,new string[] {"bullet","damage"},pos,2) {
        //
        // instanciate with default boxes + behaviors + etc.
        AddComponent(new CollisionBox(this,"myBox",false,ResourceManager.GetTextureSize(texID)))
        ;
        AddComponent(new CollisionBox(this,"bulletBox",false,ResourceManager.GetTextureSize(texID)))
        ;
        AddComponent(new BulletData(this,"BulletData"))
        ;
        Transform.SetRotation(parentRot)
        ;
        AddBehavior("moveToRot",Behaviors.MoveTowardRotation)
        ;
        AddBehavior("boundsCheck",Behaviors.DestroyOutOfBounds)
        ;
        CollisionHandler
        .myActions
        .Add(new CollisionActions("bulletBox",new List<string> {"enemyBox"},new List<CollisionAction>{CollisionBehaviors.DealDamage}))
        ;

    }
    public string DumpStats() {
        string dmg = Data.damage.ToString();
        string spd = Data.speed.ToString();
        return "dmg " + dmg + ", spd " + spd;
    }

}