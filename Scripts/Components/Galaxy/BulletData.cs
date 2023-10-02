using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame_Core.Scripts;

public class BulletData : Component {
    public float damage;
    public float speed;
    public BulletData(GameObject go, string name) : base(go,name){
        damage=1;
        speed=600;
    }
}