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
    public class Scene
    {
        protected ContentManager Content;
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        public Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public Dictionary<string, SoundEffect> SoundEffects= new Dictionary<string, SoundEffect>();
        public virtual void Initilize(ContentManager c)
        {
            Content = c;
        }

        public virtual void OnLoad()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Update(GameTime gt)
        {
            foreach (GameObject go in GameObjects.Values)
            {
                go.Update(gt);
            }
        }
    }
}
