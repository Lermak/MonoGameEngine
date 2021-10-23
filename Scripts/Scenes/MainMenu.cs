using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace MonoGame_Core.Scripts
{
    public class MainMenu : Scene
    {
        public MainMenu() : base()
        {

        }

        protected override void loadContent()
        {      
            SoundManager.SongChannels["Melody"] = Content.Load<Song>("Music/TestSong");
            MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            SoundManager.SoundEffectChannels["TestHit"] = Content.Load<SoundEffect>("Sound/TestHit").CreateInstance();

            ResourceManager.Textures = new Dictionary<string, Texture2D>();
            ResourceManager.Textures["Test"] = Content.Load<Texture2D>("Test");
            ResourceManager.Textures["Base"] = Content.Load<Texture2D>("Images/Base");

            ResourceManager.Fonts["TestFont"] = Content.Load<SpriteFont>("Fonts/TestFont");

            gameObjects = new List<GameObject>();
            //GameObjects.Add(new TestObject("Test", "testObj"));
            gameObjects.Add(new TestStaticObject("Test", new Vector2(100,100), "Test1", 1));
            gameObjects.Add(new Button("Test", "Base", "PlayButton", new Vector2(40, 40), new Vector2(500, 100), 1, null));
            gameObjects.Add(new Button("Test", "Base", "QuitButton", new Vector2(40, 40), new Vector2(500, 40), 1, Behaviors.QuitOnClick));
            base.loadContent();
        }

        public override void Update(float dt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.GetPressedKeys().Length > 0)
                {
                    SceneManager.ChangeScene(new TestScene());
                }
            }

            base.Update(dt);
        }
    }
}
