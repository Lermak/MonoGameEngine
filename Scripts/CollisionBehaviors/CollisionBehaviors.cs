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
        public static void UndoMinPen(CollisionBox a, CollisionBox b, Vector2 p, WorldObject w)
        {
            if (Math.Abs(p.X) < Math.Abs(p.Y))
            {
                w.Transform.Position.X -= p.X;
            }
            else
            {
                w.Transform.Position.Y -= p.Y;
            }
        }

        public static void PlayHitSound(CollisionBox a, CollisionBox b, Vector2 p, WorldObject w)
        {
            if (SoundManager.SoundEffectChannels["TestHit"] != null)
                if (SoundManager.SoundEffectChannels["TestHit"].State == SoundState.Stopped)
                    SoundManager.SoundEffectChannels["TestHit"].Play();
        }
    }
}
