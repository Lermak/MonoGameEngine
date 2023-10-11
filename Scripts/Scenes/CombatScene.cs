using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace MonoGame_Core.Scripts
{

    public class CombatScene : Scene
    {
        // we dont need to create a billion randoms
        Random rng = new Random();
        // idk how to elegantly do this
        /// <summary>
        /// recursively generates a random position outside of a given blackout radius
        /// </summary>
        /// <returns></returns>
        Vector2 SpawnPosRe(Vector2 center, float radius)
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

        protected override void loadContent()
        {
            //ResourceManager.AddSong("Melody", "Music/TestSong");
            //SoundManager.PlaySong("Melody");

            ResourceManager.AddTexture("Galaxy", "Images/GalaxyMap/Galaxy");
            ResourceManager.AddTexture("Ship", "Images/Galaxy/GoofyEnemyShip");
            ResourceManager.AddTexture("Base", "Images/Base");
            ResourceManager.AddTexture("BG", "Images/GalaxyMap/SpaceBG");
            ResourceManager.AddTexture("UIBar", "Images/GalaxyMap/UIBar");
            ResourceManager.AddTexture("JumpGate", "Images/GalaxyMap/JumpGate");
            ResourceManager.AddTexture("BulletTex", "Images/Bullet");

            ResourceManager.AddFont("BaseFont", "Fonts/TestFont");
        }

        protected override void loadObjects()
        {

            // todo: remove redundant player obj
            /*
            PlayerShip player = (PlayerShip)InitWorldObject(new PlayerShip("Base", "playerShip", new Vector2(0, 0)));
            */ 

            if (Player.Ship == null)
                InitWorldObject(new Player("Base", "Grid", new Vector2(15, 8)));
            else
            {
                grid = InventoryGrid.Grid; // exhaustive enough i guess :^)
                Console.WriteLine(InventoryGrid.Inventory.StoredItems.ToString());//debuggies
            }
            //
            // spawn in the player before any enemies
            PlayerShip player =
            (PlayerShip)InitWorldObject(new PlayerShip("Base", "playerShip", new Vector2(0, 0)));
            player.AddBehavior("reload", ShipBehaviors.ReloadWeapon);
            //
            // spawn in enemies, recursively find a good spawn point
            int max = 5;
            for (int i = 0; i < max; i++)
            {
                Random rng = new Random();
                Vector2 spawnPos = SpawnPosRe(ship.Transform.Position, 800);
                EnemyShip enemy 
                = (EnemyShip)InitWorldObject(new EnemyShip("Ship", "", spawnPos));
                enemy
                .AddBehavior(
                    "pointToPlayer",
                    ShipBehaviors.PointToPlayer,
                    new Component[] { ship.Transform }
                    )
                ;
                enemy.AddBehavior("shootPlayer", ShipBehaviors.EmitRandomBullet);
            }
        }
        public override void OnExit()
        {
            Globals.GalaxyMap = gameObjects;
            base.OnExit();
        }
    }
}
