using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace MonoGame_Core.Scripts
{

    public static class AnchorBehaviors
    {
        static Random rng = new Random();
        // idk how to elegantly do this
        /// <summary>
        /// recursively generates a random position outside of a 
        /// given blackout radius.
        /// <br/>
        /// <b>c[0]</b> should be the player's <b>Transform</b>.
        /// </summary>
        /// <returns></returns>
        public static Vector2 SpawnPosRe(Vector2 center, float radius)
        {
            Vector2 outPos = new Vector2(
                (float)rng.Next(((int)-radius) - 1000, (int)radius + 1000),
                rng.Next(((int)-radius) - 1000, (int)radius + 1000)
            );
            if (Vector2.Distance(outPos, center) <= radius)
            {
                outPos = SpawnPosRe(center, radius);
            } // else
            return outPos;
        }
        public static void SpawnMax(float dt, GameObject go, Component[] c = null)
        {
            Transform playerTransform = (Transform)c[0];
            SpawnAnchorData data = ((SpawnAnchor)go).Data;
            if (data.spawns.Count < data.max)
            {
                EnemyShip enemy = (EnemyShip)SceneManager.CurrentScene
                .InitGameObject(new EnemyShip(
                    "GoofyEnemyShip",
                    "",
                    SpawnPosRe(playerTransform.Position, 800)))
                ;
                enemy
                .AddBehavior(
                    "pointToPlayer",
                    ShipBehaviors.PointToPlayer,
                    new Component[] { playerTransform }
                    )
                ;
                enemy.AddBehavior(
                    "shootPlayer",
                    ShipBehaviors.EmitRandomBullet
                    )
                ;
            }
            else { }
        }
    }

}