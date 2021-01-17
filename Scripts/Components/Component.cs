using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class Component
    {
        protected string name = "component";
        public int UpdateOrder;
        public string Name { get { return name; } }
        public Component(int uo)
        {
            UpdateOrder = uo;
        }

        public virtual void Initilize()
        {

        }

        public virtual void Update(float gt)
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
