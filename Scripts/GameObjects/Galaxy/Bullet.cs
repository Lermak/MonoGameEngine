using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class Bullet : WorldObject {
    
    public Bullet(string texID,string name,Vector2 pos,float parentRot)
    : base(texID,name,new string[] {"bullet","damage"},pos,2) {
        //
        // instanciate with default boxes + behaviors + etc.
        AddComponent(new CollisionBox(this,"bulletBox",false,ResourceManager.GetTextureSize(texID)));
        this.Transform.SetRotation(parentRot);
        AddBehavior("moveWithRot",Behaviors.MoveTowardRotation);
        AddBehavior("boundsCheck",Behaviors.DestroyOutOfBounds);

    }

}