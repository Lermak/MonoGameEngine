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

            ResourceManager.AddTexture("FarmingBG", "Images/VisitSystem/FarmingBG");
            ResourceManager.AddTexture("IndustryBG", "Images/VisitSystem/IndustryBG");
            ResourceManager.AddTexture("LuxuryBG", "Images/VisitSystem/LuxuryBG");
            ResourceManager.AddTexture("JumpGateBG", "Images/VisitSystem/JumpGateBG");

            ResourceManager.AddTexture("LaunchBtn", "Images/VisitSystem/LaunchButton");
            ResourceManager.AddTexture("LaunchBtnHover", "Images/VisitSystem/LaunchButtonHover");
            ResourceManager.AddTexture("ShipBtn", "Images/VisitSystem/ShipButton");
            ResourceManager.AddTexture("ShipBtnHover", "Images/VisitSystem/ShipButtonHover");
            ResourceManager.AddTexture("TradeBtn", "Images/VisitSystem/TradeButton");
            ResourceManager.AddTexture("TradeBtnHover", "Images/VisitSystem/TradeButtonHover");
        }

        protected override void loadObjects()
        {
            if(type == GalaxyData.GalaxyType.Farming)
            {
                InitWorldObject(new WorldObject("FarmingBG", "FarmingBG", new string[] { }, new Vector2(), -0));
            }
            else if (type == GalaxyData.GalaxyType.Industry)
            {
                InitWorldObject(new WorldObject("IndustryBG", "IndustryBG", new string[] { }, new Vector2(), -0));
            }
            else if (type == GalaxyData.GalaxyType.Luxury)
            {
                InitWorldObject(new WorldObject("LuxuryBG", "LuxuryBG", new string[] { }, new Vector2(), -0));
            }
            else if (type == GalaxyData.GalaxyType.JumpGate)
            {
                InitWorldObject(new WorldObject("JumpGateBG", "JumpGateBG", new string[] { }, new Vector2(), -0));
            }
            
            
            
            InitGameObject(new Button("LaunchBtn", "LaunchBtnHover", "LaunchButton", new Vector2(0, -Globals.SCREEN_HEIGHT/2 + ResourceManager.GetTextureSize("LaunchBtn").Y/2), 1, VisitSystemBehaviors.ReturnToMap));
            InitGameObject(new Button("TradeBtn", "TradeBtnHover", "TradeButton", new Vector2(-Globals.SCREEN_WIDTH/2 + ResourceManager.GetTextureSize("TradeBtn").X/2 + 100, -Globals.SCREEN_HEIGHT / 2 + ResourceManager.GetTextureSize("LaunchBtn").Y / 2), 1, VisitSystemBehaviors.ReturnToMap));
            InitGameObject(new Button("ShipBtn", "ShipBtnHover", "ShipButton", new Vector2(Globals.SCREEN_WIDTH / 2 - ResourceManager.GetTextureSize("ShipBtn").X / 2 - 100, -Globals.SCREEN_HEIGHT / 2 + ResourceManager.GetTextureSize("LaunchBtn").Y / 2), 1, VisitSystemBehaviors.ReturnToMap));

        }
    }
}
