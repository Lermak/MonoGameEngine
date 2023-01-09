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
                if (RenderingManager.RenderTargets.Count > value)
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
                if (ResourceManager.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            }
        }

        public Transform Transform { get { return transform; } }
        public Vector2 MinPos { get { return minPos; } set { minPos = value; } }
        public Vector2 MaxPos { get { return maxPos; } set { maxPos = value; } }
        public Rectangle DrawSize { get { return drawSize; } set { drawSize = value; } }
        public Vector2 ScreenPosition { get { return (renderPosition * new Vector2(1,-1) - new Vector2(drawSize.Width / 2, drawSize.Height / 2)) * RenderingManager.GameScale + new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2) * RenderingManager.WindowScale; } }
        public Color BG_Color { get { return bg_color; } set { bg_color = value; } }
        public Camera(string name, int target, byte layer, Vector2 size, Vector2 min, Vector2 max, Vector2 pos, Vector2 screenPos) : base(name, new string[] { "camera" })
        {
            RenderTarget2D rt;
            if (target >= 0)
                rt = RenderingManager.RenderTargets[target];
            else
                rt = new RenderTarget2D(RenderingManager.GraphicsDevice, (int)Globals.SCREEN_WIDTH, (int)Globals.SCREEN_HEIGHT);
            
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
                foreach (EffectTechnique t in ResourceManager.Effects[shader].Techniques)
                {
                    foreach (EffectPass p in t.Passes)
                    {
                        p.Apply();
                    }
                }
            }

            DrawToSpriteBatch(sb);
        }

        private void DrawToSpriteBatch(SpriteBatch sb)
        {
            sb.Draw(RenderingManager.RenderTargets[Target],
                    ScreenPosition,
                    new Rectangle(0, 0, (int)(RenderingManager.RenderTargets[target].Width * RenderingManager.GameScale.X), (int)(RenderingManager.RenderTargets[target].Height * RenderingManager.GameScale.Y)),
                    Color.White,
                    0,//-Transform.Radians, //Gotta figure out rotation when it comes to attaching the transform to another object
                    (new Vector2(drawSize.Width / 2, drawSize.Height / 2) / new Vector2(RenderingManager.RenderTargets[target].Width, RenderingManager.RenderTargets[target].Height)) * RenderingManager.GameScale,
                    new Vector2(drawSize.Width, drawSize.Height) / new Vector2(RenderingManager.RenderTargets[target].Width, RenderingManager.RenderTargets[target].Height) * transform.Scale,//all items being drawn were already scaled by gamescale, no need to scale down again
                    SpriteEffects.None,
                    (float)Transform.Layer / 256f);
        }
    }
}