using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class ShipWeapon : InventoryItem
    {
        public ItemCombatData Data { get { return (ItemCombatData)GetComponent("CombatData"); } }
        static Random r = new Random();
        /// <summary>
        /// ShipWeapon Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texID"></param>
        /// <param name="pos"></param>
        /// <param name="shape"></param>
        public ShipWeapon(string name, string texID, Vector2 pos, InventoryItemShapeData.Shapes shape) : base(name, texID, pos, shape, new string[] { "ShipWeapon", "InventoryItem" })
        {
            AddComponent(new ItemCombatData(this, "CombatData", 1, 300, 1, "BulletTex"));
            AddBehavior("Fire", ShipBehaviors.ShootOnClick);
        }

        /// <summary>
        /// Generates a random ShipWeapon with a randomized shape, as well as randomized Damage, Speed, and Reload multipliers
        /// </summary>
        public static void GenRandomWeapon(Vector2 pos)
        {

            InventoryItemShapeData.Shapes s =
            (InventoryItemShapeData.Shapes)r.Next(
                Enum.GetNames(typeof(InventoryItemShapeData.Shapes)).Length - 1)
            ;

            ShipWeapon w =
            (ShipWeapon)SceneManager.CurrentScene
            .InitWorldObject(new ShipWeapon(
                Guid.NewGuid().ToString(),
                s.ToString(),
                pos,
                s
            ));
            w.Data.SpeedMult = hf_Math.NextFloat(r,1,3);
            w.Data.ReloadMult = hf_Math.NextFloat(r,1,3);
            w.Data.DamageMult = hf_Math.NextFloat(r,1,3);
        }
    }
}
