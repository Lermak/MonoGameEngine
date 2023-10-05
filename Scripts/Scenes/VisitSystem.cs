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

            ResourceManager.AddTexture("Block", "Images/Tetreminos/Block");
            ResourceManager.AddTexture("TwoLine", "Images/Tetreminos/TwoLong");
            ResourceManager.AddTexture("ThreeLine", "Images/Tetreminos/ThreeLong");
            ResourceManager.AddTexture("J", "Images/Tetreminos/J");
            ResourceManager.AddTexture("L", "Images/Tetreminos/L");
            ResourceManager.AddTexture("S", "Images/Tetreminos/S");
            ResourceManager.AddTexture("Z", "Images/Tetreminos/Z");
            ResourceManager.AddTexture("LHook", "Images/Tetreminos/LeftHook");
            ResourceManager.AddTexture("RHook", "Images/Tetreminos/RightHook");
            ResourceManager.AddTexture("Square", "Images/Tetreminos/Square");
            ResourceManager.AddTexture("T", "Images/Tetreminos/T");
            ResourceManager.AddTexture("Line", "Images/Tetreminos/Line");

            ResourceManager.AddTexture("SystemInfo", "Images/GalaxyMap/SystemInfo");

            ResourceManager.AddFont("BaseFont", "Fonts/TestFont");
        }

        protected override void loadObjects()
        {
            Vector2 btnSize = ResourceManager.GetTextureSize("Btn");
            InitGameObject(new ItemInfo("SystemInfo", "ItemInfo", new Vector2()));
            CameraManager.MainCamera.MinPos = new Vector2(0, 0);
            CameraManager.MainCamera.MaxPos = new Vector2(Globals.SCREEN_WIDTH+200, 0);
            if (Player.Ship == null)
                InitWorldObject(new Player("Grid", "Grid", new Vector2(15, 8)));
            else
            {
                InitWorldObject(Player.Ship);
                Player.Ship.SpriteRenderer.Cameras = new List<Camera>() { CameraManager.MainCamera };
                
                foreach(InventoryItem i in Player.Inventory.StoredItems)
                {
                    i.Restore();
                }
            }
            Player.Ship.Transform.SetPosition(new Vector2(100, 100));

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
                Random r = new Random();
                InventoryItemShapeData.Shapes s = (InventoryItemShapeData.Shapes)r.Next(Enum.GetNames(typeof(InventoryItemShapeData.Shapes)).Length - 1);
                InitWorldObject(new InventoryItem(Guid.NewGuid().ToString(), s.ToString(), new Vector2(-Globals.SCREEN_WIDTH * .25f + 50, -Globals.SCREEN_HEIGHT / 2 + 100), s));
                s = (InventoryItemShapeData.Shapes)r.Next(Enum.GetNames(typeof(InventoryItemShapeData.Shapes)).Length - 1);
                InitWorldObject(new InventoryItem(Guid.NewGuid().ToString(), s.ToString(), new Vector2(-Globals.SCREEN_WIDTH * .05f + 50, -Globals.SCREEN_HEIGHT / 2 + 100), s));
                s = (InventoryItemShapeData.Shapes)r.Next(Enum.GetNames(typeof(InventoryItemShapeData.Shapes)).Length - 1);
                InitWorldObject(new InventoryItem(Guid.NewGuid().ToString(), s.ToString(), new Vector2(Globals.SCREEN_WIDTH * .15f + 50, -Globals.SCREEN_HEIGHT / 2 + 100), s));
                s = (InventoryItemShapeData.Shapes)r.Next(Enum.GetNames(typeof(InventoryItemShapeData.Shapes)).Length - 1);
                InitWorldObject(new InventoryItem(Guid.NewGuid().ToString(), s.ToString(), new Vector2(Globals.SCREEN_WIDTH * .35f + 50, -Globals.SCREEN_HEIGHT / 2 + 100), s));

                wo = InitWorldObject(new TextButton("LaunchBtn", 
                    "Btn",
                    "BtnHover", 
                    "Launch", 
                    5,
                    new Vector2(-Globals.SCREEN_WIDTH / 2 + btnSize.X / 2, -Globals.SCREEN_HEIGHT / 2 + btnSize.Y / 2), 
                    new Vector2(), 
                    1, 
                    VisitSystemBehaviors.ReturnToMap));
                wo.SpriteRenderer.IsHUD = false;
                wo.Transform.SetScale(.5f, .5f);
            }
            else
            {

            }
        }
    }
}
