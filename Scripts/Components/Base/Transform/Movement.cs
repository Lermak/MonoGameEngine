using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Movement : Component
    {
        float speed;
        float rotSpeed;
        public float Speed { get { return speed; } }
        public float RotSpeed { get { return rotSpeed; } }
        public Movement(GameObject go, string name, float s, float rs) : base(go, name)
        {
            speed = s;
            rotSpeed = rs;
        }
    }
}
