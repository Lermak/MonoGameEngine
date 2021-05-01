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

            Effect prevShader = null;
            RenderTarget2D Target = null;

            graphicsDevice.SetRenderTarget(Target);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            foreach (SpriteRenderer sr in s)
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

                    graphicsDevice.SetRenderTarget(sr.Target);
                    graphicsDevice.Clear(Color.Transparent);

                    Target = sr.Target;
                    
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); 
                }

                if (sr.Shader != null)
                {
                    foreach (EffectTechnique t in sr.Shader.Techniques)
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
            spriteBatch.End();

            Sprites.Clear();
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
