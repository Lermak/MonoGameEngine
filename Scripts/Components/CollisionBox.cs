using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class CollisionBox : Component
    {
        public string Tag;
        public bool CheckCollision;
        Vector2 offset;
        Transform transform;
        CollisionHandler collisionHandler;
        float width;
        float height;

        public CollisionBox(string tag, bool checkCollision, Vector2 off, Transform transform, float width, float height, CollisionHandler ch, int uo) : base(uo)
        {
            Tag = tag;
            CheckCollision = checkCollision;
            offset = off;
            this.transform = transform;
            this.width = width;
            this.height = height;
            collisionHandler = ch;
        }

        public Vector2 TopLeft()
        {
            return transform.Position + offset;
        }

        public Vector2 TopRight()
        {
            return transform.Position + offset + new Vector2(width, 0);
        }

        public Vector2 BottomLeft()
        {
            return transform.Position + offset + new Vector2(0, height);
        }

        public Vector2 BottomRight()
        {
            return transform.Position + offset + new Vector2(width, height);
        }

        public void Resize(float width, float height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
