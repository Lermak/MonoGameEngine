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
    public class TestCombatScene : Scene
    {
        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");

            ResourceManager.AddTexture("Galaxy", "Images/GalaxyMap/Galaxy");
            ResourceManager.AddTexture("Ship", "Images/Galaxy/GoofyEnemyShip");
            ResourceManager.AddTexture("BG", "Images/GalaxyMap/SpaceBG");
            ResourceManager.AddTexture("UIBar", "Images/GalaxyMap/UIBar");
            ResourceManager.AddTexture("JumpGate", "Images/GalaxyMap/JumpGate");
            ResourceManager.AddTexture("SystemInfo", "Images/GalaxyMap/SystemInfo");

            ResourceManager.AddFont("BaseFont", "Fonts/TestFont");
        }

        protected override void loadObjects()
        {
            
            
            
        }
        public override void OnExit()
        {
            Globals.GalaxyMap = gameObjects;
            base.OnExit();
        }
    }
}
