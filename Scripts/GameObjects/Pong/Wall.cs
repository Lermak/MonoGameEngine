using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class Wall : GameObject
    {
        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }

        public Wall(string name, Vector2 position, Vector2 size) : base(name)
        {
            componentHandler.AddComponent(new Transform(0, position, size.X, size.Y, 0, 0));
            
            ComponentHandler.AddComponent(new CollisionBox(name, 0, this, Transform, true));
        }

    }
}
