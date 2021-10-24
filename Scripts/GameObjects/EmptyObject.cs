using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class EmptyObject : GameObject
    {
        public EmptyObject(string name, string[] tags) : base(name, tags)
        {

        }

        public override void Initilize()
        {
            base.Initilize();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
