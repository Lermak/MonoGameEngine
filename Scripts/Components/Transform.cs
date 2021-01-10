using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class Transform : Component
    {
        public Vector2 Position;

        public Transform(int uo, Vector2 pos) : base(uo)
        {
            Position = pos;
        }
    }
}
