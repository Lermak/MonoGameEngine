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
            ResourceManager.AddFont("BaseFont", "Fonts/TestFont");

            ResourceManager.AddTexture("FarmingBG", "Images/VisitSystem/FarmingBG");
            ResourceManager.AddTexture("IndustryBG", "Images/VisitSystem/IndustryBG");
            ResourceManager.AddTexture("LuxuryBG", "Images/VisitSystem/LuxuryBG");
            ResourceManager.AddTexture("JumpGateBG", "Images/VisitSystem/JumpGateBG");

            ResourceManager.AddTexture("ShopMenu", "Images/VisitSystem/ShopMenu");

            ResourceManager.AddTexture("Btn", "Images/VisitSystem/ButtonTemplate");
            ResourceManager.AddTexture("BtnHover", "Images/VisitSystem/ButtonTemplateHover");
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

            Vector2 btnSize = ResourceManager.GetTextureSize("Btn");

            if (type != GalaxyData.GalaxyType.JumpGate)
            {
                SellShop sell = (SellShop)InitGameObject(new SellShop("ShopMenu", 
                    "SellShop", 
                    new Vector2(Globals.SCREEN_WIDTH/2 - ResourceManager.GetTextureSize("ShopMenu").X/2 - 100, 100)));
                PurchaseShop purchase = (PurchaseShop)InitGameObject(new PurchaseShop("ShopMenu", 
                    "PurchaseShop",
                    new Vector2(-Globals.SCREEN_WIDTH / 2 + ResourceManager.GetTextureSize("ShopMenu").X/2 + 100, 100)));

                InitGameObject(new TextButton("Launch", 
                    "Btn",
                    "BtnHover", 
                    "Launch", 
                    5,
                    new Vector2(0, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2), 
                    new Vector2(), 
                    1, 
                    VisitSystemBehaviors.ReturnToMap));

                InitGameObject(new TextButton("Ship",
                    "Btn",
                    "BtnHover",
                    "Ship",
                    5,
                    new Vector2(-Globals.SCREEN_WIDTH / 2 + btnSize.X / 2 + 100, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2),
                    new Vector2(),
                    1,
                    VisitSystemBehaviors.ReturnToMap));

                InitGameObject(new TextButton("Trade",
                    "Btn",
                    "BtnHover",
                    "Trade",
                    5,
                    new Vector2(Globals.SCREEN_WIDTH / 2 - btnSize.X / 2 - 100, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2),
                    new Vector2(),
                    1,
                    VisitSystemBehaviors.Trade,
                    new Component[] { sell.SpriteRenderer, purchase.SpriteRenderer }));
            }
            else
            {

            }
        }
    }
}
