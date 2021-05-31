using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public abstract class Behavior
    {
        protected string name;
        protected GameObject gameObject;
        public int UpdateOrder;

        public string Type { get { return this.GetType().Name; } }
        public string Name { get { return name; } }
        public GameObject GameObject { get { return gameObject; } }
        public Behavior(GameObject go, int uo, string name)
        {
            gameObject = go;
            this.name = name;
            UpdateOrder = uo;
        }
        public virtual void Update(float gt)
        {

        }

        public virtual void OnDestroy()
        {

        }
    }
}
