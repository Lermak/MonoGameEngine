using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public class Scene
    {
        protected Vector2 size;//Scene size must never be smaller than the rendering size
        protected ContentManager Content;
        public Vector2 Size { get { return size; } }

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        public Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public Dictionary<string, SoundEffect> SoundEffects= new Dictionary<string, SoundEffect>();
        public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        public virtual void Initilize(ContentManager c)
        {
            size = new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT);
            Content = c;
            CollisionManager.Clear();
            RenderingManager.Clear();
            SoundManager.Clear();
            Camera.Initilize();
        }

        public virtual void OnLoad()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Update(float gt)
        {
            foreach (GameObject go in GameObjects.Values)
            {
                go.Update(gt);
            }
        }
    }
}
