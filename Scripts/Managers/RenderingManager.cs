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
                (int)WIDTH,
                (int)HEIGHT,
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

            IEnumerable<SpriteRenderer> s = Sprites.OrderByDescending(s => s.Target)
                                        .ThenBy(s => s.Shader)
                                        .ThenBy(s => s.Layer)
                                        .ThenBy(s => s.Transform.Position.Y)
                                        .ThenBy(s => s.OrderInLayer);

            string prevShader = "";
            int Target = -1;

            SetTarget(Target);

            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            foreach (SpriteRenderer sr in s)
            {
                if (sr.Visible)
                {
                    if (sr.Shader != prevShader)
                    {
                        if (sr.Target == Target)
                        {
                            spriteBatch.End();
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                        }

                        prevShader = sr.Shader;
                    }

                    if (sr.Target != Target)
                    {
                        spriteBatch.End();

                        SetTarget(sr.Target);
                        graphicsDevice.Clear(Color.Transparent);

                        Target = sr.Target;

                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    }

                    if (sr.Shader != null)
                    {
                        foreach (EffectTechnique t in SceneManager.CurrentScene.Effects[sr.Shader].Techniques)
                        {
                            foreach (EffectPass p in t.Passes)
                            {
                                p.Apply();
                                if (sr.IsHUD)
                                    DrawHUDElement(sr);
                                else
                                    DrawGameElement(sr);
                            }
                        }
                    }
                    else
                    {
                        if (sr.IsHUD)
                            DrawHUDElement(sr);
                        else
                            DrawGameElement(sr);
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

            foreach (EffectTechnique t in SceneManager.CurrentScene.Effects["CRT"].Techniques)
            {
                foreach (EffectPass p in t.Passes)
                {
                    p.Apply();
                    spriteBatch.Draw(RenderTargets[0],
                    new Vector2(),
                    new Rectangle(0, 0, (int)(WIDTH * WindowScale.X), (int)(HEIGHT * WindowScale.Y)),
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    new Vector2(1, 1),
                    SpriteEffects.None,
                    0);
                }
            }

            spriteBatch.End();
        }

        private static void SetTarget(int Target)
        {
            if (Target == -1)
                graphicsDevice.SetRenderTarget(null);
            else
                graphicsDevice.SetRenderTarget(RenderTargets[Target]);
        }

        private static void DrawHUDElement(SpriteRenderer sr)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.Textures[sr.Texture],
                sr.WorldPosition() + (new Vector2(WIDTH / 2, HEIGHT / 2) * WindowScale),
                sr.DrawRect(),
                new Color(sr.Color.R - (int)GlobalFade, sr.Color.G - (int)GlobalFade, sr.Color.B - (int)GlobalFade, sr.Color.A),
                sr.Transform.Rotation,
                new Vector2(0, 0),
                WindowScale * sr.Transform.Scale,
                sr.SpriteEffect,
                1);
        }

        private static void DrawGameElement(SpriteRenderer sr)
        {
            spriteBatch.Draw(SceneManager.CurrentScene.Textures[sr.Texture],
                (sr.WorldPosition() - Camera.Position + (new Vector2(WIDTH / 2, HEIGHT / 2) * WindowScale)),
                sr.DrawRect(),
                new Color(sr.Color.R - (int)GlobalFade, sr.Color.G - (int)GlobalFade, sr.Color.B - (int)GlobalFade, sr.Color.A),
                sr.Transform.Rotation,
                new Vector2(sr.Transform.Width / 2, sr.Transform.Height / 2),
                GameScale * sr.Transform.Scale,
                sr.SpriteEffect,
                sr.Layer/100);
        }
    }
}
