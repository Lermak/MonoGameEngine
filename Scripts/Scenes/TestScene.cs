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

        public override void Initilize(ContentManager c)
        {
            base.Initilize(c);

            size = new Vector2(2100, 1080);

            SoundManager.SongChannels["Melody"] = Content.Load<Song>("Music/TestSong");
            MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            SoundManager.SoundEffectChannels["TestHit"] = Content.Load<SoundEffect>("Sound/TestHit").CreateInstance();

            Effects["TestShader"] = Content.Load<Effect>("Shaders/TestShader");
            Effects["BlueShader"] = Content.Load<Effect>("Shaders/BlueShader");
            Effects["CRT"] = Content.Load<Effect>("Shaders/CRTShader");

            Textures = new Dictionary<string, Texture2D>();
            Textures["Test"] = Content.Load<Texture2D>("Images/Test");
            Textures["BG"] = Content.Load<Texture2D>("Images/MainMenuBG");

            GameObjects = new Dictionary<string, GameObject>();
            GameObjects.Add("test", new TestObject("Test", "testObj"));
            GameObjects.Add("testStatic", new TestStaticObject("Test", "testStatic"));
            GameObjects.Add("testStatic2", new TestStaticObject("Test", "testStatic"));
            GameObjects.Add("BG", new WorldObject("BG", "Background"));
            ((WorldObject)GameObjects["BG"]).SpriteRenderer.SetDrawArea(1920, 1080);
            ((WorldObject)GameObjects["BG"]).Transform.Place(new Vector2());
            ((WorldObject)GameObjects["BG"]).Transform.Resize(960, 540);
            ((WorldObject)GameObjects["testStatic2"]).Transform.Place(new Vector2(10, 10));
            ((WorldObject)GameObjects["testStatic2"]).SpriteRenderer.Shader = "BlueShader";
        }
    }
}
