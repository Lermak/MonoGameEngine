using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
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
