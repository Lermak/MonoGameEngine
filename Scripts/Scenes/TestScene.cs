using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class TestScene:Scene
    {
        public TestScene():base()
        {
            
        }

        public override void Initilize(ContentManager c)
        {
            Content = c;

            SoundManager.SongChannels["Melody"] = Content.Load<Song>("Music/TestSong");
            MediaPlayer.Play(SoundManager.SongChannels["Melody"]);

            SoundManager.SoundEffectChannels["TestHit"] = Content.Load<SoundEffect>("Sound/TestHit").CreateInstance();

            Textures = new Dictionary<string, Texture2D>();
            Textures["Test"] = Content.Load<Texture2D>("Test");

            GameObjects = new Dictionary<string, GameObject>();
            GameObjects.Add("TestPlayer", new TestPlayer("Test", "TestPlayer"));
            GameObjects.Add("TestObject2", new SampleWorldObject("Test", "TestObject2"));
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
    }
}
