using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Xna.Framework;

public class BulletData : Component {
    float damage;
    public BulletData() : base() {
        damage=1;
    }
}