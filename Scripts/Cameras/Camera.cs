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
        Rectangle drawArea;
        string shader = "";
        Vector2 screenPosition;
        byte layer;
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
                if (SceneManager.CurrentScene.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            }
        }

        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }
        public Vector2 Position { get { return Transform.Position * RenderingManager.WindowScale; } }
        public Vector2 MinPos { get { return minPos; } }
        public Vector2 MaxPos { get { return maxPos; } }
        public Rectangle DrawArea { get { return drawArea; } set { drawArea = value; } }
        public Vector2 ScreenPosition { get { return screenPosition; } set { screenPosition = value; } }
        public byte Layer { get { return layer; } set { layer = value; } }

        public Camera(string tag, int target, byte layer, float width, float height, Vector2 size, Vector2 min, Vector2 max) : base(tag)
        {
            Transform t = new Transform(this, 0, new Vector2(), width, height, 0, 0);
            minPos = min;
            maxPos = max;
            Target = target;
            drawArea = new Rectangle(0, 0, (int)(RenderingManager.WIDTH * RenderingManager.WindowScale.X), (int)(RenderingManager.HEIGHT * RenderingManager.WindowScale.Y));
            componentHandler.AddComponent(t);

            screenPosition = new Vector2(RenderingManager.WIDTH / 2, RenderingManager.HEIGHT / 2);
        }

        public override void Update(float gt)
        {
            base.Update(gt);
            clamp();
        }

        public void SetMinPos(Vector2 min)
        {
            minPos = min;
        }

        public void SetMaxPos(Vector2 max)
        {
            maxPos = max;
        }

        void clamp()
        {
            if (Transform.Position.X > MaxPos.X)
                Transform.Place(new Vector2(MaxPos.X, Transform.Position.Y));
            else if (Transform.Position.X < MinPos.X)
                Transform.Place(new Vector2(MinPos.X, Transform.Position.Y));

            if (Transform.Position.Y > MaxPos.Y)
                Transform.Place(new Vector2(Transform.Position.X, MaxPos.Y));
            else if (Transform.Position.Y < MinPos.Y)
                Transform.Place(new Vector2(Transform.Position.X, MinPos.Y));
        }

        public void Draw(SpriteBatch sb)
        {
            if (shader != "")
                foreach (EffectTechnique t in SceneManager.CurrentScene.Effects[shader].Techniques)
                {
                    foreach (EffectPass p in t.Passes)
                    {
                        p.Apply();
                        sb.Draw(RenderingManager.RenderTargets[Target],
                                (screenPosition - new Vector2(Transform.Width / 2, Transform.Height / 2)) * RenderingManager.WindowScale,
                                new Rectangle(0, 0, (int)(RenderingManager.RenderTargets[target].Width * RenderingManager.WindowScale.X), (int)(RenderingManager.RenderTargets[target].Height * RenderingManager.WindowScale.Y)),
                                Color.White,
                                Transform.Radians,
                                new Vector2(),
                                new Vector2(Transform.Width, Transform.Height) / new Vector2(RenderingManager.RenderTargets[target].Width, RenderingManager.RenderTargets[target].Height),
                                SpriteEffects.None,
                                Layer / 256);
                    }
                }
            else
                sb.Draw(RenderingManager.RenderTargets[Target],
                        (screenPosition - new Vector2(Transform.Width / 2, Transform.Height / 2)) * RenderingManager.WindowScale,
                        new Rectangle(0, 0, (int)(RenderingManager.RenderTargets[target].Width * RenderingManager.WindowScale.X), (int)(RenderingManager.RenderTargets[target].Height * RenderingManager.WindowScale.Y)),
                        Color.White,
                        Transform.Radians,
                        new Vector2(),
                        new Vector2(Transform.Width, Transform.Height) / new Vector2(RenderingManager.RenderTargets[target].Width, RenderingManager.RenderTargets[target].Height),
                        SpriteEffects.None,
                        Layer / 256);
        
        }
    }
}