using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace MonoGame_Core.Scripts
{
    public class SpawnAnchor : GameObject
    {

        public SpawnAnchorData Data { get { return (SpawnAnchorData) GetComponent("AnchorData"); } }
        public SpawnAnchor(string name)
        : base(name, new string[] { "anchor", "worldspawn" })
        {
            // data
            AddComponent(new SpawnAnchorData(this, "AnchorData"));
            // spawns
            AddBehavior("Spawn", AnchorBehaviors.SpawnMax);
        }
    }
}