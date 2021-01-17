using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class Behavior
    {
        public readonly string Name = "behavior";      
        public int UpdateOrder;

        public Behavior(int uo)
        {
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
