using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class ShipWeapon : InventoryItem
    {
        public ShipWeapon(string name, string texID, Vector2 pos, InventoryItemShapeData.Shapes shape) : base(name, texID, pos, shape, new string[] { "ShipWeapon", "InventoryItem" })
        {
            AddComponent(new ItemCombatData(this, "CombatData", 1, 300, 1, "base"));
            AddBehavior("Fire", ShipBehaviors.ShootOnClick);
        }
    }
}
