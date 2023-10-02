using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Manageages the scenes, scene transitions and the state of the scene
    /// </summary>
    public static class SceneManager
    {
        /// <summary>
        /// Determines how to handle the game loop based on what the scene is performing
        /// </summary>
        public enum State { Running, Paused, SceneOut, SceneIn };
        public static Scene CurrentScene = new TestScene();
        public static Scene NextScene = null;
        public static State SceneState;
        public static List<Scene> SavedScenes = new List<Scene>();

        public static void Initilize(Scene s)
        {
            SceneState = State.SceneIn;
            CurrentScene = s;
            InitilizeCurrentScene();
            CurrentScene.OnLoad();
        }   

        public static void ChangeScene(Scene s, bool save = false)
        {
            SceneState = State.SceneOut;
            CurrentScene.OnExit();
            NextScene = s;
            if (save && !SavedScenes.Contains(CurrentScene))
                SavedScenes.Add(CurrentScene);
            else if (SavedScenes.Contains(CurrentScene))
                SavedScenes.Remove(CurrentScene);
        }
        public static void ChangeScene(string name, bool save = false)
        {
            Scene s = SavedScenes.Where(s => s.Name == name).FirstOrDefault();
            if (s != null)
            {
                SceneState = State.SceneOut;
                CurrentScene.OnExit();
                NextScene = s;
                if (save && !SavedScenes.Contains(CurrentScene))
                    SavedScenes.Add(CurrentScene);
                else if (SavedScenes.Contains(CurrentScene))
                    SavedScenes.Remove(CurrentScene);
            }
        }

        public static GameObject AddObject(GameObject go)
        {
            return CurrentScene.AddGameObject(go);
        }
        public static WorldObject AddObject(WorldObject wo)
        {
            return CurrentScene.AddWorldObject(wo);
        }
        public static List<GameObject> GetObjects(string tag)
        {
            return CurrentScene.GetObjects(tag);
        }

        public static GameObject GetObject(string name)
        {
            return CurrentScene.GetObject(name);
        }

        public static GameObject AddObject(GameObject go)
        {
            return CurrentScene.AddGameObject(go);
        }
        public static WorldObject AddObject(WorldObject wo)
        {
            return CurrentScene.AddWorldObject(wo);
        }
        public static List<GameObject> GetObjects(string tag)
        {
            return CurrentScene.GetObjects(tag);
        }

        public static GameObject GetObject(string name)
        {
            return CurrentScene.GetObject(name);
        }

        /// <summary>
        /// Run the update of the current scene, or load the next schene if it is null
        /// </summary>
        /// <param name="dt">Game Time</param>
        public static void Update(float dt)
        {
            if(CurrentScene == null)
            {
                CurrentScene = NextScene;
                NextScene = null;
                CurrentScene.Initilize();
                CurrentScene.OnLoad();
                SceneState = State.SceneIn;
            }
            else if(CurrentScene != null)
                CurrentScene.Update(dt);         
        }

        public static void InitilizeCurrentScene()
        {
            CurrentScene.Initilize();
        }
    }
}
