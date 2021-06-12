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

        public List<GameObject> ToAdd = new List<GameObject>();
        public List<GameObject> GameObjects = new List<GameObject>();

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
        public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        public Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        public virtual void Initilize(ContentManager c)
        {
            size = new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT);
            Content = c;
            CollisionManager.Initilize();
            RenderingManager.Clear();
            SoundManager.Initilize();
            CoroutineManager.Initilize();   

            loadContent();
        }

        protected virtual void loadContent()
        {
            foreach (GameObject go in GameObjects)
            {
                go.Initilize();
            }

            RenderingManager.Sort();//Sort the items in the renderingManager, this should only be done when new items are added
        }

        public virtual void OnLoad()
        {
            CoroutineManager.AddCoroutine(Coroutines.FadeInSceneTransision(), "FadeIn", 0, true);
        }

        public virtual void OnExit()
        {
            CoroutineManager.AddCoroutine(Coroutines.FadeOutSceneTransision(), "FadeOut", 0, true);
        }

        public virtual void Update(float gt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
                SceneRunning(gt);
            else if (SceneManager.SceneState == SceneManager.State.Paused)
                ScenePaused(gt);
        }

        public virtual void SceneRunning(float gt)
        {
            List<GameObject> destroy = new List<GameObject>();
            foreach (GameObject go in GameObjects)
            {
                go.Update(gt);
                if (go.ToDestroy)
                    destroy.Add(go);
            }
            foreach (GameObject go in destroy)
            {
                go.OnDestroy();
                GameObjects.Remove(go);
            }
            foreach (GameObject go in ToAdd)
            {
                go.Initilize();
                GameObjects.Add(go);
            }
            ToAdd.Clear();
        }

        public virtual void ScenePaused(float gt)
        {

        }
    }
}
