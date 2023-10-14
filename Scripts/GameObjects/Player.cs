using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class Player : WorldObject
    {
        public static Player Ship = null;
        public static InventoryGridData Inventory { get { return (InventoryGridData)Ship.componentHandler.Get("Grid"); } }
        public static ShipData.ShipState ShipState { get { return ((ShipData)Ship.componentHandler.Get("ShipData")).MyState; } }

        public Player(string texID, string name, Vector2 gridSize) : base(texID, name, new string[] { "InventoryGrid" }, new Vector2(0, 0), 1)
        {
            if (Ship == null)
            {
                Ship = this;
                AddBehavior("Scale", Behaviors.ManualScale);
                //AddBehavior("Move", Behaviors.MoveWithRot);
                AddBehavior("Move", Behaviors.DriveStrafe);
                AddBehavior("Transition", ShipBehaviors.Transition);
                AddComponent(new Movement(this, "movement", 300, 360));
                AddComponent(new ShipData(this, "ShipData"));               
                AddComponent(new InventoryGridData(this, "Grid", (int)gridSize.X, (int)gridSize.Y));
            }
        }
        
    }
}