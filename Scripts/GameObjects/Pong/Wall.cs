using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class Wall : GameObject
    {
        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }

        public Wall(string name, Vector2 position, Vector2 size, byte layer) : base(name)
        {
            componentHandler.AddComponent(new Transform(this, 0, position, size.X, size.Y, 0, layer));
            
            ComponentHandler.AddComponent(new CollisionBox(this, name, 0, this, Transform, true));
        }

    }
}
