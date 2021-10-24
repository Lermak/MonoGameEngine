using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class AnimationData : Component
    {
        public float TimeSinceLastFrameChange = 0;
        public float AnimationSpeed;
        public bool AnimationDirection = true;
        public SpriteRenderer SpriteRenderer;

        public AnimationData(GameObject go, float speed) : base(go, "animationData")
        {
            SpriteRenderer = (SpriteRenderer)go.ComponentHandler.Get("spriteRenderer");
            AnimationSpeed = speed;
        }      

        public void SetAnimation(int anim)
        {
            if (anim <= SpriteRenderer.Animations && anim > 0)
                SpriteRenderer.CurrentAnimation = anim;
        }

        public void SetFrame(int frame)
        {
            if (frame <= SpriteRenderer.Frames && frame > 0)
                SpriteRenderer.CurrentFrame = frame;
        }
    }
}
