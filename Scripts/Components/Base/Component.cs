using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public abstract class Component
    {
        protected string name = "component";
        protected GameObject gameObject;

        public string Type { get { return this.GetType().Name; } }
        public string Name { get { return name; } }
        public GameObject GameObject { get { return gameObject; } }

        public Component(GameObject go, string name)
        {
            gameObject = go;
            this.name = name;
        }

        public virtual void Initilize()
        {

        }

        public virtual void OnCreate()
        {

        }

        public virtual void OnDestroy()
        {

        }
    }
}
