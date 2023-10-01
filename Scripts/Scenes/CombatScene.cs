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
            //
            // spawn in the player first
            Ship player = (Ship)InitWorldObject(new Ship("Base", "playerShip", new Vector2(0, 0)));
            //
            // spawn in enemies
            // currently also populates middle of the map,
            // dangerous to player
            int enemyCount = 5;
            for (int i = 0; i < enemyCount; i++)
            {
                Random rng = new Random();
                Vector2 spawnPos = new Vector2(
                    rng.Next(
                        ((int)-Globals.SCREEN_WIDTH / 2) + 100,
                        ((int)Globals.SCREEN_WIDTH / 2) - 100
                        ),
                    rng.Next(
                        ((int)-Globals.SCREEN_HEIGHT / 2) + 100,
                        ((int)Globals.SCREEN_HEIGHT / 2) - 100
                        )
                    );
                WorldObject newEnemy = InitWorldObject(new WorldObject("Ship", "", new string[] { "enemy" }, spawnPos, 1));
                newEnemy.AddBehavior(
                    "playerPoint",
                    ShipBehaviors.PointToPlayer,
                    new Component[] { player.Transform }
                );
                newEnemy.AddBehavior(
                    "randomShoot",
                    ShipBehaviors.EmitRandomBullet
                    );
            }


        }
        public override void OnExit()
        {
            Globals.GalaxyMap = gameObjects;
            base.OnExit();
        }
    }
}
