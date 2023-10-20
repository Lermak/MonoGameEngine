using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class Camera : GameObject
    {
        int target;
        Vector2 minPos;
        Vector2 maxPos;
        Rectangle drawSize;
        Transform transform;
        Vector2 renderPosition;
        string shader = "";
        Color bg_color = Color.Black;

        public int Target
        {
            get { return target; }
            set
            {
                if (Globals.RenderingManager.RenderTargets.Count > value)
                    target = value;
                else
                    target = -1;
            }
        }
        public string Shader
        {
            get { return shader; }
            set
            {
                if (Globals.ResourceManager.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            }
        }

        public Transform Transform { get { return transform; } }
        public Vector2 MinPos { get { return minPos; } set { minPos = value; } }
        public Vector2 MaxPos { get { return maxPos; } set { maxPos = value; } }
        public Rectangle DrawSize { get { return drawSize; } set { drawSize = value; } }
        public Vector2 ScreenPosition { get { return (renderPosition * new Vector2(1,-1) - new Vector2(drawSize.Width / 2, drawSize.Height / 2)) * Globals.RenderingManager.GameScale + new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2) * Globals.RenderingManager.WindowScale; } }
        public Color BG_Color { get { return bg_color; } set { bg_color = value; } }
        public Camera(string name, int target, byte layer, Vector2 size, Vector2 min, Vector2 max, Vector2 pos, Vector2 screenPos) : base(name, new string[] { "camera" })
        {
            if (target >= 0 && target >= Globals.RenderingManager.RenderTargets.Count())
            {
                while (Globals.RenderingManager.RenderTargets.Count() <= target)
                {
                    Globals.RenderingManager.RenderTargets.Add(new RenderTarget2D(Globals.RenderingManager.GraphicsDevice, (int)Globals.SCREEN_WIDTH, (int)Globals.SCREEN_HEIGHT, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.PreserveContents));
                }
            }
            renderPosition = screenPos;
            transform = new Transform(this, pos, 0, layer);
            minPos = min;
            maxPos = max;
            Target = target;
            drawSize = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            componentHandler.Add(transform);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            clamp();
        }

        void clamp()
        {
            if (Transform.Position.X > MaxPos.X)
                Transform.SetPosition(new Vector2(MaxPos.X, Transform.Position.Y));
            else if (Transform.Position.X < MinPos.X)
                Transform.SetPosition(new Vector2(MinPos.X, Transform.Position.Y));

            if (Transform.Position.Y > MaxPos.Y)
                Transform.SetPosition(new Vector2(Transform.Position.X, MaxPos.Y));
            else if (Transform.Position.Y < MinPos.Y)
                Transform.SetPosition(new Vector2(Transform.Position.X, MinPos.Y));
        }

        public void Draw(SpriteBatch sb)
        {
            if (shader != "")
            {
                foreach (EffectTechnique t in Globals.ResourceManager.Effects[shader].Techniques)
                {
                    foreach (EffectPass p in t.Passes)
                    {
                        p.Apply();
                        DrawToSpriteBatch(sb);
                    }
                }
            }
            else
                DrawToSpriteBatch(sb);
        }

        private void DrawToSpriteBatch(SpriteBatch sb)
        {
            sb.Draw(Globals.RenderingManager.RenderTargets[Target],
                    ScreenPosition,
                    new Rectangle(0, 0, (int)(Globals.RenderingManager.RenderTargets[target].Width * Globals.RenderingManager.GameScale.X), (int)(Globals.RenderingManager.RenderTargets[target].Height * Globals.RenderingManager.GameScale.Y)),
                    Color.White,
                    0,//-Transform.Radians, //Gotta figure out rotation when it comes to attaching the transform to another object
                    (new Vector2(drawSize.Width / 2, drawSize.Height / 2) / new Vector2(Globals.RenderingManager.RenderTargets[target].Width, Globals.RenderingManager.RenderTargets[target].Height)) * Globals.RenderingManager.GameScale,
                    new Vector2(drawSize.Width, drawSize.Height) / new Vector2(Globals.RenderingManager.RenderTargets[target].Width, Globals.RenderingManager.RenderTargets[target].Height) * transform.Scale,//all items being drawn were already scaled by gamescale, no need to scale down again
                    SpriteEffects.None,
                    (float)Transform.Layer / 256f);
        }
    }
}