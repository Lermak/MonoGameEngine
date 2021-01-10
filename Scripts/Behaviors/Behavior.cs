using Microsoft.Xna.Framework;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class Behavior
    {
        public int UpdateOrder;
        public Behavior(int uo)
        {
            UpdateOrder = uo;
        }
        public virtual void Update(GameTime gt)
        {

        }

        public virtual void OnDestroy()
        {

        }
    }
}
