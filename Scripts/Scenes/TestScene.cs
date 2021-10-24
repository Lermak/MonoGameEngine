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
            base.loadContent();

            size = new Vector2(2100, 1080);
            CollisionManager.Initilize();

            CameraManager.Cameras[0].SetMinPos(Size/2*-1);
            CameraManager.Cameras[0].SetMaxPos(Size / 2);

            SoundManager.SongChannels["Melody"] = Content.Load<Song>("Music/TestSong");
            //MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            SoundManager.SoundEffectChannels["TestHit"] = Content.Load<SoundEffect>("Sound/TestHit").CreateInstance();

            ResourceManager.Effects["TestShader"] = Content.Load<Effect>("Shaders/TestShader");
            ResourceManager.Effects["BlueShader"] = Content.Load<Effect>("Shaders/BlueShader");
            ResourceManager.Effects["CRT"] = Content.Load<Effect>("Shaders/CRTShader");

            ResourceManager.Textures = new Dictionary<string, Texture2D>();
            ResourceManager.Textures["Test"] = Content.Load<Texture2D>("Images/Test");
            ResourceManager.Textures["PeaShooter"] = Content.Load<Texture2D>("Images/PeaShooter");
            ResourceManager.Textures["Base"] = Content.Load<Texture2D>("Images/Base");
            ResourceManager.Textures["BG"] = Content.Load<Texture2D>("Images/Background");

            ResourceManager.Fonts["TestFont"] = Content.Load<SpriteFont>("Fonts/TestFont");

            CameraManager.Cameras.Add(new Camera("CRTCamera", 0, 0,
                480, 
                270,
                new Vector2(480, 270),
                new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * -1,
                new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * 1));

            CameraManager.Cameras[1].BehaviorHandler.Add("ScreenShake", Behaviors.ScreenShake, new Component[] { CameraManager.Cameras[1].Transform });

            InitWorldObject(new TestObject("PeaShooter", "testObj"));
            WorldObject tso = InitWorldObject(new TestStaticObject("Base", new Vector2(200, 200), "Test1", 1));
            //tso.Transform.AttachToTransform(((WorldObject)gameObjects[0]).Transform);

            tso = InitWorldObject(new TestStaticObject("Base", new Vector2(200, -100), "Test2", 1));
            //tso.Transform.AttachToTransform(((WorldObject)gameObjects[0]).Transform);

            tso = InitWorldObject(new WorldObject("BG", "Background", new string[] { }, new Vector2(1920,1080), new Vector2(), 0));
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
