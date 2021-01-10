using Microsoft.Xna.Framework;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class GameObject
    {
        protected ComponentHandler componentHandler;
        protected BehaviorHandler behaviorHandler;
        protected string tag;
        public string Tag { get { return tag; } }
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
