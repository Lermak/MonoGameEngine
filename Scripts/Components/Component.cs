using Microsoft.Xna.Framework;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
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
