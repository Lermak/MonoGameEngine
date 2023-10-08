﻿using System;
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
                (float) rng.Next(((int)-radius) - 1000,(int)radius + 1000),
                rng.Next(((int)-radius) - 1000,(int)radius + 1000)
            );
            if (Vector2.Distance(outPos,center) <= radius) {
                outPos = SpawnPosRe(center,radius);
            } // else
            return outPos;
        }
        
        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");

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

            


            if (Player.Ship != null)
            {

                InventoryGridData inv = Player.Inventory;
                //
                // grab the weapons
                List<InventoryItem> weapons =
                inv.FetchItemsByType(ItemData.ItemTypes.Combat);
            }
            else { }

            //
            // spawn in the player first
            PlayerShip player = (PlayerShip)InitWorldObject(new PlayerShip("Base", "playerShip", new Vector2(0, 0)));
            //
            // spawn in enemies currently also populates
            // middle of the map, dangerous to player
            int max = 3;
            for (int i = 0; i < max; i++)
            {
                Random rng = new Random();
                Vector2 spawnPos = SpawnPosRe(player.Transform.Position,800);
                EnemyShip enemy = (EnemyShip)InitWorldObject(new EnemyShip("Ship", "", spawnPos));
                enemy.AddBehavior("pointToPlayer", ShipBehaviors.PointToPlayer, new Component[] { player.Transform });
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
