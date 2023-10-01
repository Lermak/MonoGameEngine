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
            ResourceManager.AddTexture("Grid", "Images/Inventory/Grid");

            ResourceManager.AddTexture("Btn", "Images/VisitSystem/ButtonTemplate");
            ResourceManager.AddTexture("BtnHover", "Images/VisitSystem/ButtonTemplateHover");

            ResourceManager.AddTexture("Test", "Images/Test");

        }

        protected override void loadObjects()
        {
            Vector2 btnSize = ResourceManager.GetTextureSize("Btn");

            CameraManager.MainCamera.MinPos = new Vector2(0, 0);
            CameraManager.MainCamera.MaxPos = new Vector2(Globals.SCREEN_WIDTH+200, 0);
            if (InventoryGrid.Grid == null)
                InitWorldObject(new InventoryGrid("Grid", "Grid", new Vector2(15, 8)));
            else
            {
                InitWorldObject(InventoryGrid.Grid);
                InventoryGrid.Grid.SpriteRenderer.Cameras = new List<Camera>() { CameraManager.MainCamera };
                
                foreach(InventoryItem i in InventoryGrid.Inventory.StoredItems)
                {
                    i.Restore();
                }
            }
            InventoryGrid.Grid.Transform.SetPosition(new Vector2(Globals.SCREEN_WIDTH+200, 100));

            WorldObject wo = InitWorldObject(new TextButton("GoToSystemBtn",
                "Btn",
                "BtnHover",
                "Return",
                5,
                new Vector2(Globals.SCREEN_WIDTH / 2 + btnSize.X / 2 + 300, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2),
                new Vector2(),
                1,
                VisitSystemBehaviors.GotoSystem));
            wo.SpriteRenderer.IsHUD = false;

            if (type == GalaxyData.GalaxyType.Farming)
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


            if (type != GalaxyData.GalaxyType.JumpGate)
            {
                SellShop sell = (SellShop)InitGameObject(new SellShop("ShopMenu", 
                    "SellShop", 
                    new Vector2(Globals.SCREEN_WIDTH/2 - ResourceManager.GetTextureSize("ShopMenu").X/2 - 100, 100)));
                PurchaseShop purchase = (PurchaseShop)InitGameObject(new PurchaseShop("ShopMenu", 
                    "PurchaseShop",
                    new Vector2(-Globals.SCREEN_WIDTH / 2 + ResourceManager.GetTextureSize("ShopMenu").X/2 + 100, 100)));

                wo = InitWorldObject(new TextButton("LaunchBtn", 
                    "Btn",
                    "BtnHover", 
                    "Launch", 
                    5,
                    new Vector2(0, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2), 
                    new Vector2(), 
                    1, 
                    VisitSystemBehaviors.ReturnToMap));
                wo.SpriteRenderer.IsHUD = false;

                wo = InitWorldObject(new TextButton("GoToShipBtn",
                    "Btn",
                    "BtnHover",
                    "Ship",
                    5,
                    new Vector2(-Globals.SCREEN_WIDTH / 2 + btnSize.X / 2 + 100, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2),
                    new Vector2(),
                    1,
                    VisitSystemBehaviors.GotoShip));
                wo.SpriteRenderer.IsHUD = false;

                wo = InitWorldObject(new TextButton("TradeBtn",
                    "Btn",
                    "BtnHover",
                    "Trade",
                    5,
                    new Vector2(Globals.SCREEN_WIDTH / 2 - btnSize.X / 2 - 100, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2),
                    new Vector2(),
                    1,
                    VisitSystemBehaviors.Purchase,
                    new Component[] { sell.SpriteRenderer, purchase.SpriteRenderer }));
                wo.SpriteRenderer.IsHUD = false;
            }
            else
            {

            }
        }
    }
}
