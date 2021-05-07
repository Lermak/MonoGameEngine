using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;


namespace MonoGame_Core.Scripts
{
    public static class RenderingManager
    {
        public enum RenderOrder { TopDown, YSort, Isometric }
        public const float WIDTH = 1920;
        public const float HEIGHT = 1080;

        public static Vector2 GameScale { get { return WindowScale * BaseScale; } }
        public static Vector2 BaseScale = new Vector2(1, 1);
        public static Vector2 WindowScale = new Vector2(1, 1);
        public static float GlobalFade = 255;
        public static RenderOrder RenderingOrder = RenderOrder.TopDown;

        public static List<RenderTarget2D> RenderTargets;
        public static List<SpriteRenderer> Sprites;
        private static SpriteBatch spriteBatch;
        private static GraphicsDevice graphicsDevice;
        

        public static void Initilize(GraphicsDevice gd)
        {
            graphicsDevice = gd;
            spriteBatch = new SpriteBatch(graphicsDevice);
            Sprites = new List<SpriteRenderer>();
            RenderTargets = new List<RenderTarget2D>();

            RenderTargets.Add(new RenderTarget2D(graphicsDevice,
                (int)1920,
                (int)1080,
                false,
                graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24,
                0,
                RenderTargetUsage.PreserveContents));
        }

        public static void Clear()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            Sprites = new List<SpriteRenderer>();
        }

        public static void Draw(float gt)
        {
            var x = graphicsDevice.GetRenderTargets();
            WindowScale = new Vector2(graphicsDevice.Viewport.Width / WIDTH, graphicsDevice.Viewport.Height / HEIGHT);

            string prevShader = "";
            int Target = -1;

            SetTarget(Target);

            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            IEnumerable<Camera> cameras = CameraManager.Cameras.OrderByDescending(s => s.Target);
           
            foreach (Camera c in cameras)
            {
                foreach (SpriteRenderer sr in Sprites)
                {
                    if (sr.Visible && sr.Cameras.Contains(c))
                    {
                        if (sr.Shader != prevShader)
                        {
                            if (c.Target == Target)
                            {
                                spriteBatch.End();
                                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                            }

                            prevShader = sr.Shader;
                        }

                        if (c.Target != Target)
                        {
                            spriteBatch.End();

                            SetTarget(c.Target);
                            graphicsDevice.Clear(Color.Transparent);

                            Target = c.Target;

                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                        }

                        if (sr.Shader != "")
                        {
                            foreach (EffectTechnique t in SceneManager.CurrentScene.Effects[sr.Shader].Techniques)
                            {
                                foreach (EffectPass p in t.Passes)
                                {
                                    p.Apply();
                                    sr.Draw(spriteBatch, c);
                                }
                            }
                        }
                        else
                        {
                            sr.Draw(spriteBatch, c);
                        }
                    }
                }
            }
            spriteBatch.End();

            if(Target != -1)
            {
                Target = -1;
                SetTarget(-1);
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            CameraManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static void Sort()
        {
            IEnumerable<Camera> cameras = CameraManager.Cameras.OrderByDescending(s => s.Target);

            IEnumerable<SpriteRenderer> s = Sprites;

            foreach (Camera c in cameras)
            {
                if (RenderingOrder == RenderOrder.TopDown)
                    s = Sprites.OrderBy(s => s.Shader)
                                .ThenBy(s => s.Transform.Layer)
                                .ThenBy(s => s.OrderInLayer)
                                .Where(s => s.Cameras.Contains(c))
                                .Where(s => Vector2.Distance(s.Transform.Position, c.Transform.Position) <= s.Transform.Radius + c.Transform.Radius);
                else if (RenderingOrder == RenderOrder.YSort)
                    s = Sprites.OrderBy(s => s.Shader)
                                .ThenBy(s => s.Transform.Layer)
                                .ThenBy(s => s.Transform.Position.Y)
                                .ThenBy(s => s.OrderInLayer)
                                .Where(s => s.Cameras.Contains(c))
                                .Where(s => Vector2.Distance(s.Transform.Position, c.Transform.Position) <= s.Transform.Radius + c.Transform.Radius);

                else if (RenderingOrder == RenderOrder.Isometric)
                {

                }
            }

            Sprites = s.ToList();
        }

        private static void SetTarget(int Target)
        {
            if (Target == -1)
                graphicsDevice.SetRenderTarget(null);
            else
                graphicsDevice.SetRenderTarget(RenderTargets[Target]);
        }
    }
}
