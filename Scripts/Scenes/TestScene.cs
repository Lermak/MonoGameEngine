using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
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
        public TestScene():base()
        {
            
        }

        protected override void loadContent()
        {
            size = new Vector2(2100, 1080);
            CameraManager.Cameras[0].SetMinPos(Size/2*-1);
            CameraManager.Cameras[0].SetMaxPos(Size / 2);

            SoundManager.SongChannels["Melody"] = Content.Load<Song>("Music/TestSong");
            MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            SoundManager.SoundEffectChannels["TestHit"] = Content.Load<SoundEffect>("Sound/TestHit").CreateInstance();

            Effects["TestShader"] = Content.Load<Effect>("Shaders/TestShader");
            Effects["BlueShader"] = Content.Load<Effect>("Shaders/BlueShader");
            Effects["CRT"] = Content.Load<Effect>("Shaders/CRTShader");

            Textures = new Dictionary<string, Texture2D>();
            Textures["Test"] = Content.Load<Texture2D>("Images/Test");
            Textures["BG"] = Content.Load<Texture2D>("Images/MainMenuBG");

            Fonts["TestFont"] = Content.Load<SpriteFont>("Fonts/TestFont");

            CameraManager.Cameras.Add(new Camera("CRTCamera", 0, 0,
                new Transform(0, new Vector2(0, 0), 480, 270, 0),
                new Vector2(480, 270),
                new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * -1,
                new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * 1));

            CameraManager.Cameras[1].BehaviorHandler.AddBehavior(new ScreenShake(0, "screenShake", CameraManager.Cameras[1].Transform));

            GameObjects = new Dictionary<string, GameObject>();
            GameObjects.Add("test", new TestObject("Test", "testObj"));
            GameObjects.Add("testStatic", new TestStaticObject("Test", "testStatic"));
            GameObjects.Add("testStatic2", new TestStaticObject("Test", "testStatic"));
            GameObjects.Add("BG", new WorldObject("BG", "Background", new Vector2(1920,1080), new Vector2()));
            ((WorldObject)GameObjects["BG"]).SpriteRenderer.Cameras.Add(CameraManager.Cameras[1]);
            ((WorldObject)GameObjects["testStatic2"]).Transform.Place(new Vector2(10, 10));
            ((WorldObject)GameObjects["testStatic2"]).SpriteRenderer.Shader = "BlueShader";



            CameraManager.Cameras[1].ScreenPosition = new Vector2(480, 270) / 2;
            CameraManager.Cameras[1].Shader = "CRT";

            base.loadContent();
        }

        public override void SceneRunning(float gt)
        {
            base.SceneRunning(gt);
        }
    }
}
