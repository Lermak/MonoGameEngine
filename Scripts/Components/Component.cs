using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class Component
    {
        public int UpdateOrder;
        public Component(int uo)
        {
            UpdateOrder = uo;
        }

        public virtual void Initilize()
        {

        }

        public virtual void Update(GameTime gt)
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
