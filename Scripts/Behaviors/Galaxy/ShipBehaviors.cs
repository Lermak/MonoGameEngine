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
        public static void Transition(float dt, GameObject go, Component[] c = null)
        {
            if(InputManager.IsTriggered(ConfigurationManager.Configuration.Keybindings["space"]))
            {
                if(Player.ShipState == ShipData.ShipState.Playing)
                    CoroutineManager.Add(Coroutines.ModifyShipTransition((WorldObject)go), "modifyTransition", 0, true);
                else if(Player.ShipState == ShipData.ShipState.Sorting)
                    CoroutineManager.Add(Coroutines.PlayGameTransition((WorldObject)go), "modifyTransition", 0, true);
            }
        }
        public static void ShootOnClick(float dt, GameObject go, Component[] c = null)
        {
            Vector2 mousePos = InputManager.MousePos;
            Transform t = ((WorldObject)go).Transform;
            ItemCombatData combatData = (ItemCombatData)go.GetComponent("CombatData");

            if (InputManager.IsPressed(InputManager.MouseKeys.Left) && combatData.Reloading == false)
            {
                Console.WriteLine("shot detected");
                CoroutineManager.Add(Coroutines.Reload(combatData), "Reload" + go.Name, 0, true);
                Bullet b =
                (Bullet) SceneManager
                .AddObject(new Bullet("BulletTex", "", t.Position, t.RotationDegrees))
                ;
                //Console.WriteLine(b.DumpStats());
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

            t.Radians = hf_Math.GetAngleRad(playerTF.Position, t.Position) + 90 * (float)Math.PI / 180;
            if (t.Parent != null)
                t.Rotate(t.Radians - t.Parent.Radians);

        }
        /// <summary>
        /// <i>Ship-specific.</i><br />
        /// 
        /// moves a <b>Ship</b> towards a rotation factoring in its speed percentage
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void MoveShipTowardRotation(float dt, GameObject go, Component[] c = null)
        {
            Transform st = (Transform)go.GetComponent("transform");
            RigidBody srb = (RigidBody)go.GetComponent("rigidBody");
            ShipData sdata = (ShipData)go.GetComponent("ShipData");

            srb.MoveVelocity = hf_Math.RadToUnit(st.Radians + 90 * (float)Math.PI / 180) * dt * sdata.speed;
        }
        /// <summary>
        ///  spawns a bullet instance with EnemyShip's rotation at random intervals
        ///  <br/> this does not work currently, dont use this for the love of god
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
            /*if (shoot == 60 & enemyData.reload == 100) {
                SceneManager.CurrentScene.AddWorldObject(new Bullet("BulletTex", "", enemyTF.Position, enemyTF.RotationDegrees));
                enemyData.reload = 0;
            }*/

        }
        //
        /// <summary>
        /// makes sure the thing dies when dead
        ///
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void DieWhenDead(float dt, GameObject go, Component[] c=null) {
            EnemyShip eS = (EnemyShip)go;
            EnemyShipData eSData = (EnemyShipData)eS.GetComponent("enemyShipData");

            if (!eS.ToDestroy)
            {
                if (eSData.health > 0)
                {
                    // dont do nuffin
                }
                else
                {
                    eS.Destroy();
                }
            }
        }
    }
}