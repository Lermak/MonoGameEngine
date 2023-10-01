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
            ResourceManager.AddTexture("BulletTex", "Images/Bullet");
            ResourceManager.AddTexture("PeaShooter", "Images/PeaShooter");
            ResourceManager.AddTexture("Grid", "Images/Inventory/Grid");
            ResourceManager.AddTexture("BG", "Images/Background");

            ResourceManager.AddFont("TestFont", "Fonts/TestFont");

            ResourceManager.AddTexture("Block", "Images/Test");
        }

        protected override void loadObjects()
        {
            InitWorldObject(new InventoryGrid("Grid", "Grid", new Vector2(10,10)));

            InventoryItem item = (InventoryItem)InitWorldObject(new InventoryItem("TestInventoryItem", "Test", new Vector2(), InventoryItemShapeData.SHAPE.Line));
            InventoryItem item2 = (InventoryItem)InitWorldObject(new InventoryItem("TestInventoryItem2", "Test", new Vector2(100,100), InventoryItemShapeData.SHAPE.Line));
            InventoryItem item3 = (InventoryItem)InitWorldObject(new InventoryItem("TestInventoryItem3", "Test", new Vector2(-100,-100), InventoryItemShapeData.SHAPE.Line));
        }
    }
}
