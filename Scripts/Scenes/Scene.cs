using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public class Scene
    {
        protected Vector2 size;//Scene size must never be smaller than the rendering size
        public Vector2 Size { get { return size; } }

        protected List<GameObject> toAdd = new List<GameObject>();
        protected List<GameObject> gameObjects = new List<GameObject>();

        public IList<GameObject> GameObjects { get { return gameObjects.AsReadOnly(); } }

        /// <summary>
        /// Add a gameObject during scene loading
        /// </summary>
        /// <param name="go">Object to add</param>
        /// <returns>The object added</returns>
        public GameObject InitGameObject(GameObject go)
        {
            VerifyUniqueName(go);
            go.Initilize();
            gameObjects.Add(go);
            return gameObjects[^1];
        }

        /// <summary>
        /// Add a worldObject during scene loading
        /// </summary>
        /// <param name="wo">Object to add</param>
        /// <returns>The object added</returns>
        public WorldObject InitWorldObject(WorldObject wo)
        {
            VerifyUniqueName(wo);
            wo.Initilize();
            gameObjects.Add(wo);
            return (WorldObject)gameObjects[^1];
        }

        /// <summary>
        /// Add a gameObject at runtime
        /// </summary>
        /// <param name="go">Object to add</param>
        /// <returns>The object added</returns>
        public GameObject AddGameObject(GameObject go)
        {
            VerifyUniqueName(go);
            toAdd.Add(go);
            return toAdd[^1];
        }

        /// <summary>
        /// Add a worldObject at runtime
        /// </summary>
        /// <param name="wo">Object to add</param>
        /// <returns>The object added</returns>
        public WorldObject AddWorldObject(GameObject wo)
        {
            VerifyUniqueName(wo);
            toAdd.Add(wo);
            return (WorldObject)toAdd[^1];
        }

        public List<GameObject> GetObjects(string tag)
        {
            return gameObjects.Where(o => o.Tags.Contains(tag)).ToList();
        }

        public GameObject GetObject(string name)
        {
            return gameObjects.Where(o => o.Name == name).FirstOrDefault();
        }

        public virtual void Initilize()
        {
            size = new Vector2(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT);
            CollisionManager.Initilize();
            RenderingManager.Initilize();
            SoundManager.Initilize();
            CoroutineManager.Initilize();
            CameraManager.Initilize();

            loadContent();
            loadObjects();

            RenderingManager.Sort();//Sort the items in the renderingManager, this should only be done when new items are added
        }

        protected virtual void loadContent()
        {

        }

        protected virtual void loadObjects()
        {

        }

        public virtual void OnLoad()
        {
            CoroutineManager.Add(Coroutines.FadeInSceneTransision(), "FadeIn", 0, true);
        }

        public virtual void OnExit()
        {
            CoroutineManager.Add(Coroutines.FadeOutSceneTransision(), "FadeOut", 0, true);
        }

        public virtual void Update(float dt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
                SceneRunning(dt);
            else if (SceneManager.SceneState == SceneManager.State.Paused)
                ScenePaused(dt);
        }

        public virtual void SceneRunning(float dt)
        {
            List<GameObject> destroy = new List<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                if (!go.ToDestroy)
                    go.Update(dt);
                if (go.ToDestroy)
                    destroy.Add(go);
            }
            foreach (GameObject go in destroy)
            {
                go.OnDestroy();
                gameObjects.Remove(go);
            }
            foreach (GameObject go in toAdd)
            {
                go.Initilize();
                gameObjects.Add(go);
            }
            toAdd.Clear();
        }

        public virtual void ScenePaused(float dt)
        {

        }

        private void VerifyUniqueName(GameObject go)
        { 
            if (go.Name != "")
            {
                if (gameObjects.Where(o => o.Name == go.Name).Count() > 0)
                {
                    throw new System.Exception("An object with name '" + go.Name + "' already exists in the current scene");
                }
                if (toAdd.Where(o => o.Name == go.Name).Count() > 0)
                {
                    throw new System.Exception("An object with name '" + go.Name + "' is already being added to the scene");
                }
            }
        }
    }
}
