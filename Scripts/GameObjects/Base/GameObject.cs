using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class GameObject
    {
        protected ComponentHandler componentHandler;
        protected BehaviorHandler behaviorHandler;
        protected string tag;
        protected bool destroy = false;

        public bool ToDestroy { get { return destroy; } }
        public string Tag { get { return tag; } }
        public ComponentHandler ComponentHandler { get { return componentHandler; } }
        public BehaviorHandler BehaviorHandler { get { return behaviorHandler; } }

        public GameObject(string tag)
        {
            this.tag = tag;
            behaviorHandler = new BehaviorHandler(this);
            componentHandler = new ComponentHandler(this);
        }

        public virtual void Initilize()
        {
            componentHandler.Initilize();
            behaviorHandler.Inizilize();
        }

        public virtual void Update(float gt)
        {
            if (destroy)
            {
                OnDestroy();
            }
            else
            {
                behaviorHandler.Update(gt);
            }
        }

        public virtual void OnCreate()
        {

        }

        public void Destroy()
        {
            destroy = true;
        }

        public virtual void OnDestroy()
        {
            behaviorHandler.OnDestroy();
            componentHandler.OnDestroy();
        }
    }
}
