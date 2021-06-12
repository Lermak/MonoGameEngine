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
        public static void UndoMinPen(Collider a, Collider b, Vector2 p)
        {
            if (Math.Abs(p.Y) >= Math.Abs(p.X))
                ((WorldObject)a.GameObject).Transform.Move(new Vector2(0,p.Y));
            else
                ((WorldObject)a.GameObject).Transform.Move(new Vector2(p.X, 0));
        }
        public static void Reflect(Collider a, Collider b, Vector2 p)
        {
            UndoMinPen(a, b, p);
            ((WorldObject)a.GameObject).RigidBody.MoveVelocity = (((WorldObject)a.GameObject).RigidBody.MoveVelocity * 1.1f);
            if(Math.Abs(p.Y) >= Math.Abs(p.X))
                ((WorldObject)a.GameObject).RigidBody.MoveVelocity = (((WorldObject)a.GameObject).RigidBody.MoveVelocity * new Vector2(1, -1));
            else
                ((WorldObject)a.GameObject).RigidBody.MoveVelocity = (((WorldObject)a.GameObject).RigidBody.MoveVelocity * new Vector2(-1, 1));
        }
    }
}
