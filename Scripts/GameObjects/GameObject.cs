using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class GameObject
    {
        protected ComponentHandler componentHandler;
        protected BehaviorHandler behaviorHandler;
        protected string tag;

        public string Tag { get { return tag; } }
        public ComponentHandler ComponentHandler { get { return componentHandler; } }
        public BehaviorHandler BehaviorHandler { get { return behaviorHandler; } }

        public GameObject(string tag)
        {
            this.tag = tag;
            behaviorHandler = new BehaviorHandler();
            componentHandler = new ComponentHandler();
        }

        public virtual void Initilize()
        {
            componentHandler.Initilize();
            behaviorHandler.Inizilize();
        }

        public virtual void Update(GameTime gt)
        {
            componentHandler.Update(gt);
            behaviorHandler.Update(gt);
        }

        public virtual void OnCreate()
        {

        }

        public virtual void OnDestroy()
        {
            behaviorHandler.OnDestroy();
            componentHandler.OnDestroy();
            SceneManager.CurrentScene.GameObjects.Remove(tag);
        }
    }
}
