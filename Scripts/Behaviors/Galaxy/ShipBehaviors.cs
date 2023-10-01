using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// To be used inside a Ship class
    /// </summary>
    public static class ShipBehaviors
    {
        
        public static void ShootOnClick(float dt, GameObject go, Component[] c = null)
        {
            Vector2 mousePos = InputManager.MousePos;
            Transform shipTF = (Transform)go.GetComponent("transform");
            ShipData shipData = (ShipData)go.GetComponent("ShipData");

            if (InputManager.IsTriggered(InputManager.MouseKeys.Left))
            {
                Bullet newBullet =(Bullet) SceneManager.CurrentScene.AddWorldObject(new Bullet("BulletTex", "", shipTF.Position, shipTF.RotationDegrees));
                newBullet.CollisionHandler.myActions.Add(
                    new CollisionActions(
                        "myBox",
                        new List<string> {"enemyBox"},
                        new List<CollisionAction> {CollisionBehaviors.DealDamage}
                        )
                    );
                
            }
        }
        /// <summary>
        /// rotates an object towards a player's position.
        /// 
        /// Player transform should be <b>c[0]</b>
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void PointToPlayer(float dt, GameObject go, Component[] c=null) {
            Transform t = ((WorldObject)go).Transform;
            Transform playerTF = (Transform )c[0];

            t.Radians = hf_Math.GetAngleDeg(playerTF.Position, t.Position) + 90 * (float)Math.PI / 180;
            if (t.Parent != null)
                t.Rotate(t.Radians - t.Parent.Radians);

        }
        /// <summary>
        /// constantly increases the reload value on a ship until it hits 100.00
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void ReloadWeapon(float dt, GameObject go, Component[] c=null) {

            ShipData data = (ShipData)((Ship)go).GetComponent("ShipData");
            
            if (data.reload < 100) {
                data.reload += dt * 50;
            }


        }
        /// <summary>
        /// <i>Ship-specific.</i><br />
        /// 
        /// moves a <b>Ship</b> towards a rotation factoring in its speed percentage
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void MoveTowardRotation(float dt, GameObject go, Component[] c = null)
        {
            Transform st = (Transform)go.GetComponent("transform");
            RigidBody srb = (RigidBody)go.GetComponent("rigidBody");
            ShipData sdata = (ShipData)go.GetComponent("ShipData");

            srb.MoveVelocity = hf_Math.RadToUnit(st.Radians + 90 * (float)Math.PI / 180) * dt * sdata.speed;
        }
        /// <summary>
        ///  spawns a bullet instance with EnemyShip's rotation at random intervals
        ///  
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void EmitRandomBullet(float dt, GameObject go, Component[] c = null) {
            Random rng = new Random();

            Transform enemyTF = ((WorldObject)go).Transform;
            EnemyShipData enemyData = (EnemyShipData) go.GetComponent("enemyShipData");
            // on average 1 in every 120 frames the parent should shoot a bullet
            int shoot = rng.Next(120);
            // use any int here, 60 is a nice number right in the middle of 120
            if (shoot == 60 & enemyData.reload == 100) {
                SceneManager.CurrentScene.AddWorldObject(new Bullet("BulletTex", "", enemyTF.Position, enemyTF.RotationDegrees));
                enemyData.reload = 0;
            }

        }

    }
}