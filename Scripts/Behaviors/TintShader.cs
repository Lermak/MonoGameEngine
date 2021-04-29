using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class TintShader : Behavior
    {
        SpriteRenderer spriteRenderer;
        Color tint = new Color(255, 255, 255);
        float updateTimer = 0;
        public TintShader(int uo, string name, SpriteRenderer sr) : base(uo, name)
        {
            spriteRenderer = sr;
        }

        public override void Update(float gt)
        {
            updateTimer += gt;
            if (updateTimer > 2)
            {
                Random rnd = new Random();
                tint = new Color(255, 255, 255);
                
                updateTimer = 0;
            }
            spriteRenderer.Shader.Parameters["TintColor"].SetValue(tint.ToVector4());
        }
    }
}
