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
        public const float WIDTH = 1920;
        public const float HEIGHT = 1080;

        public static Vector2 GameScale { get { return WindowScale * BaseScale; } }
        public static Vector2 BaseScale = new Vector2(1, 1);
        public static Vector2 WindowScale = new Vector2(1, 1);
        public static float GlobalFade = 255;

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

            IEnumerable<SpriteRenderer> s = Sprites.OrderBy(s => s.Shader)
                                        .ThenBy(s => s.Layer)
                                        .ThenBy(s => s.Transform.Position.Y)
                                        .ThenBy(s => s.OrderInLayer);

            IEnumerable<Camera> cameras = CameraManager.Cameras.OrderByDescending(s => s.Target);
            string prevShader = "";
            int Target = -1;

            SetTarget(Target);

            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            foreach(Camera c in cameras)
            {
                foreach (SpriteRenderer sr in s)
                {
                    if (sr.Visible && sr.Cameras.Contains(c))
                    {
                        if (sr.Shader != prevShader)
                        {
                            if(c.Target == Target)
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

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            CameraManager.Draw(spriteBatch);
            spriteBatch.End();
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
