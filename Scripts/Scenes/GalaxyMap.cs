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
    public class GalaxyMap : Scene
    {
        protected override void loadContent()
        {
            // music
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");
            // tex
            ResourceManager.AddTexture("BG", "Images/GalaxyMap/SpaceBG");
            ResourceManager.AddTexture("Ship", "Images/GalaxyMap/Ship");
            ResourceManager.AddTexture("UIBar", "Images/GalaxyMap/UIBar");
            ResourceManager.AddTexture("Galaxy", "Images/GalaxyMap/Galaxy");
            ResourceManager.AddTexture("combat", "Images/Galaxy/GoofyEnemyShip");
            ResourceManager.AddTexture("JumpGate", "Images/GalaxyMap/JumpGate");
            ResourceManager.AddTexture("SystemInfo", "Images/GalaxyMap/SystemInfo");
            // fonties
            ResourceManager.AddFont("BaseFont", "Fonts/TestFont");
        }

        protected override void loadObjects()
        {
            if (Globals.GalaxyMap != null)
            {
                foreach (GameObject g in Globals.GalaxyMap)
                {
                    ((WorldObject)g).SpriteRenderer.Cameras = new List<Camera> { CameraManager.MainCamera };
                    InitGameObject(g);
                }
            }
            else
            {
                InitWorldObject(new ShipMarker("Ship", "Player", new Vector2(-900, 100)));
                InitWorldObject(new WorldObject("BG", "Background", new string[] { }, new Vector2(), -0));
                InitWorldObject(new UIBar("UIBar", "UIBar", new Vector2(0, (-Globals.SCREEN_HEIGHT / 2) + ResourceManager.GetTextureSize("UIBar").Y / 2)));
                InitWorldObject(new JumpGate("JumpGate", "JumpGate", new Vector2((Globals.SCREEN_WIDTH / 2) + ResourceManager.GetTextureSize("JumpGate").X * 0.15f, 100)));
                InitWorldObject(new SystemInfo("SystemInfo", "SystemInfo", new Vector2(0, (-Globals.SCREEN_HEIGHT / 2) + ResourceManager.GetTextureSize("SystemInfo").Y / 2)));

                Random r = new Random();

                for (int z = 0; z < 4; ++z)
                {
                    int c = r.Next(2, 4); // get number of planets in this "column"
                    for (int i = 0; i < c; ++i)
                    {
                        InitWorldObject(new GalaxyNode("Galaxy", "Planet" + z + i, new Vector2(-600 + z * 400 + r.Next(-100, 50), 400 - 800 / c * i + r.Next(-100, 50)), z));
                    }
                }
                Vector2 btnSize = ResourceManager.GetTextureSize("combat");
                InitGameObject(new TextButton("test combat", "combat", "combat", "TestCombat", 5, new Vector2(0, 0), new Vector2(), 1, Behaviors.LoadSceneOnClick));
            }

        }
        public override void OnExit()
        {
            Globals.GalaxyMap = gameObjects;
            base.OnExit();
        }
    }
}
