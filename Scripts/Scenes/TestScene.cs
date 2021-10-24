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
    public class TestScene:Scene
    {
        protected override void loadContent()
        {
            size = new Vector2(2100, 1080);
            CameraManager.Cameras[0].SetMinPos(Size / 2 * -1);
            CameraManager.Cameras[0].SetMaxPos(Size / 2);

            ResourceManager.AddSong("Melody", "Music/TestSong");
            //MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            ResourceManager.AddSoundEffect("TestHit", "Sound/TestHit");

            ResourceManager.AddEffect("TestShader","Shaders/TestShader");
            ResourceManager.AddEffect("BlueShader", "Shaders/BlueShader");
            ResourceManager.AddEffect("CRT", "Shaders/CRTShader");

            ResourceManager.AddTexture("Test", "Images/Test");
            ResourceManager.AddTexture("PeaShooter", "Images/PeaShooter");
            ResourceManager.AddTexture("Base", "Images/Base");
            ResourceManager.AddTexture("BG", "Images/Background");

            ResourceManager.AddFont("TestFont", "Fonts/TestFont");
        }

        protected override void loadObjects()
        {
            CameraManager.Cameras.Add(new Camera("CRTCamera", 0, 0,
                                        480,
                                        270,
                                        new Vector2(480, 270),
                                        new Vector2(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT) * -1,
                                        new Vector2(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT) * 1));

            CameraManager.Cameras[1].BehaviorHandler.Add("ScreenShake", Behaviors.ScreenShake, new Component[] { CameraManager.Cameras[1].Transform });

            InitWorldObject(new TestObject("PeaShooter", "testObj"));
            WorldObject tso = InitWorldObject(new TestStaticObject("Base", new Vector2(200, 200), "Test1", 1));
            //tso.Transform.AttachToTransform(((WorldObject)gameObjects[0]).Transform);

            tso = InitWorldObject(new TestStaticObject("Base", new Vector2(200, -100), "Test2", 1));
            //tso.Transform.AttachToTransform(((WorldObject)gameObjects[0]).Transform);
            tso.Transform.SetRotation(45);
            tso = InitWorldObject(new WorldObject("BG", "Background", new string[] { }, new Vector2(), 0));
            tso.SpriteRenderer.Transform.Layer = 0;
            tso.SpriteRenderer.Cameras.Add(CameraManager.Cameras[1]);

            CameraManager.Cameras[1].ScreenPosition = new Vector2(480, 270) / 2;
            CameraManager.Cameras[1].Shader = "CRT";

            TiledImporter.LoadFromFile(this, @"E:\Programming\C#\MonoGame\MonoGame Core\Content\Tiled\Test.tmx");

        }

        public override void SceneRunning(float dt)
        {
            base.SceneRunning(dt);
        }
    }
}
