using MonoGame_Core.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
public class SpawnAnchorData : ShipData {
    public int max;
    public List<WorldObject> spawns = null;
    
    public SpawnAnchorData(GameObject go, string name) : base(go,name) {
        max = 1;
    }
}