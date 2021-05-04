using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public static class CollisionBehaviors
    {
        public static void UndoMinPen(CollisionBox a, CollisionBox b, Vector2 p)
        {
            ((WorldObject)a.GameObject).Transform.Move(p);
        }
    }
}
