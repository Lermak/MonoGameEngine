﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Movement : Component
    {
        float speed;
        public float Speed { get { return speed; } }
        public Movement(GameObject go, int uo, string name, float s) : base(go, uo, name)
        {
            speed = s;
        }
    }
}
