using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public class InventoryScene : Scene
    {
        protected override void loadContent()
        {
            size = new Vector2(2100, 1080);
            CameraManager.Cameras[0].MinPos = Size / -2;
            CameraManager.Cameras[0].MaxPos = Size / 2;

            ResourceManager.AddSong("Melody", "Music/TestSong");
            //MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            ResourceManager.AddSoundEffect("TestHit", "Sound/TestHit");

            ResourceManager.AddTexture("Test", "Images/Test");
            ResourceManager.AddTexture("PlayerTex", "Images/Galaxy/GoofyEnemyShip");
            ResourceManager.AddTexture("PeaShooter", "Images/PeaShooter");
            ResourceManager.AddTexture("Base", "Images/Base");
            ResourceManager.AddTexture("BG", "Images/Background");

            ResourceManager.AddFont("TestFont", "Fonts/TestFont");

            ResourceManager.AddTexture("Block", "Images/Test");
        }

        protected override void loadObjects()
        {
            if (Globals.inventoryGrid == null)
            {
                Globals.inventoryGrid = (InventoryGrid)InitWorldObject(new InventoryGrid("BG", "Background"));
            }
            InventoryItem item = new InventoryItem("TestInventoryItem", InventoryItem.SHAPE.Line, InventoryItem.DIRECTION.Right, new Vector2(0,3), new string[] { "TestInventoryItem" });
            if (Globals.inventoryGrid.canPlaceItem(item) ) { Globals.inventoryGrid.placeItem(item); }

            Ship player = (Ship) InitWorldObject( new Ship("PlayerTex","Player",new Vector2(0,0)));

            

        }
        public override void SceneRunning(float dt)
        {
            base.SceneRunning(dt);
        }
    }
}
