using MonoGame_Core.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
public class EnemyShipData : ShipData {
    public float health;
    
    public EnemyShipData(GameObject go, string name) : base(go,name) {
        health=5;
    }
}