using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class Behavior
    {
        protected string name;
        public int UpdateOrder;

        public string Type { get { return this.GetType().Name; } }
        public string Name { get { return name; } }

        public Behavior(int uo, string name)
        {
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
