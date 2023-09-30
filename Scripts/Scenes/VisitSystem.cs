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
    public class VisitSystem : Scene
    {
        private GalaxyData.GalaxyType type;
        public VisitSystem(GalaxyData.GalaxyType type)
        {
            this.type = type;
        }

        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");

            ResourceManager.AddTexture("Galaxy", "Images/GalaxyMap/Galaxy");
            ResourceManager.AddTexture("Ship", "Images/GalaxyMap/Ship");
            ResourceManager.AddTexture("BG", "Images/GalaxyMap/SpaceBG");
            ResourceManager.AddTexture("UIBar", "Images/GalaxyMap/UIBar");
            ResourceManager.AddTexture("JumpGate", "Images/GalaxyMap/JumpGate");

            ResourceManager.AddTexture("PlayBtn", "Images/Default UI/btn_play");
            ResourceManager.AddTexture("SelPlayBtn", "Images/Default UI/btn_play_sel");
        }

        protected override void loadObjects()
        {
            if(type == GalaxyData.GalaxyType.Farming)
            {

            }
            else if (type == GalaxyData.GalaxyType.Industry)
            {

            }
            else if (type == GalaxyData.GalaxyType.Luxury)
            {

            }
            else if (type == GalaxyData.GalaxyType.JumpGate)
            {

            }
            InitGameObject(new Button("PlayBtn", "SelPlayBtn", "PlayButton", new Vector2(500, 100), 1, VisitSystemBehaviors.ReturnToMap));
        }
    }
}
