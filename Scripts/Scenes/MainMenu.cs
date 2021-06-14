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

            Textures = new Dictionary<string, Texture2D>();
            Textures["Test"] = Content.Load<Texture2D>("Test");
            Textures["Base"] = Content.Load<Texture2D>("Images/Base");

            Fonts["TestFont"] = Content.Load<SpriteFont>("Fonts/TestFont");

            GameObjects = new List<GameObject>();
            //GameObjects.Add(new TestObject("Test", "testObj"));
            GameObjects.Add(new TestStaticObject("Test", 1));
            GameObjects.Add(new Button("Test", "Base", "PlayButton", new Vector2(40, 40), new Vector2(500, 100), 1, null));
            GameObjects.Add(new Button("Test", "Base", "QuitButton", new Vector2(40, 40), new Vector2(500, 40), 1, Behaviors.QuitOnClick));
            base.loadContent();
        }

        public override void Update(float gt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.GetPressedKeys().Length > 0)
                {
                    SceneManager.ChangeScene(new TestScene());
                }
            }

            base.Update(gt);
        }
    }
}
