﻿using System.Collections.Generic;
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
        public Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
        public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        public virtual void Initilize(ContentManager c)
        {
            size = new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT);
            Content = c;
            CollisionManager.Clear();
            RenderingManager.Clear();
            SoundManager.Clear();
            Camera.Initilize();

            CoroutineManager.Clear();
            CoroutineManager.AddCoroutine(FadeIn(), "FadeIn");
            CoroutineManager.AddCoroutine(FadeOut(), "FadeOut");
        }

        public virtual void OnLoad()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Update(float gt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
                SceneRunning(gt);
            else if (SceneManager.SceneState == SceneManager.State.Paused)
                ScenePaused(gt);
            else if (SceneManager.SceneState == SceneManager.State.SceneIn)
                SceneEnter(gt);
            else if (SceneManager.SceneState == SceneManager.State.SceneOut)
                SceneExit(gt);
        }

        public virtual void SceneRunning(float gt)
        {
            foreach (GameObject go in GameObjects.Values)
            {
                go.Update(gt);
            }
        }

        public virtual void ScenePaused(float gt)
        {

        }

        public virtual void SceneEnter(float gt)
        {
            if(!CoroutineManager.IsRunning("FadeIn"))
                CoroutineManager.Start("FadeIn");
        }

        public virtual void SceneExit(float gt)
        {
            CoroutineManager.Start("FadeOut");
        }

        protected IEnumerator<bool> FadeIn()
        {
            while (RenderingManager.GlobalFade > 0)
            { 
                RenderingManager.GlobalFade -= 128 * TimeManager.DeltaTime;

                if (RenderingManager.GlobalFade < 0)
                {
                    RenderingManager.GlobalFade = 0;
                    SceneManager.SceneState = SceneManager.State.Running;
                }
                yield return false;
            }
            yield return true;
        }

        protected IEnumerator<bool> FadeOut()
        {
            while (RenderingManager.GlobalFade < 255)
            {
                RenderingManager.GlobalFade += 128 * TimeManager.DeltaTime;
                if (RenderingManager.GlobalFade > 255)
                {
                    RenderingManager.GlobalFade = 255;
                    SceneManager.SceneState = SceneManager.State.SceneIn;

                    SceneManager.CurrentScene.OnExit();
                    SceneManager.CurrentScene = SceneManager.NextScene;
                    SceneManager.NextScene = null;

                    SceneManager.InitilizeCurrentScene();
                    SceneManager.CurrentScene.OnLoad();
                }
                yield return false;
            }

            yield return true;
        }
    }
}
