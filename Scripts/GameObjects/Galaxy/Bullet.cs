using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class Bullet : WorldObject {
    
    public Bullet(string texID,string name,Vector2 pos,float parentRot)
    : base(texID,name,new string[] {"bullet","damage"},pos,1) {
        //
        // instanciate with default boxes + behaviors + etc.
        AddComponent(new CollisionBox(this,"myBox",false,ResourceManager.GetTextureSize(texID)));
        this.Transform.SetRotation(parentRot);
        AddBehavior("moveWithRot",Behaviors.MoveTowardRotation);

    }

}