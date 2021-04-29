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

        public static List<SpriteRenderer> Sprites;
        public static List<SpriteRenderer> HUD;
        private static SpriteBatch spriteBatch;
        private static GraphicsDevice graphicsDevice; 

        public static void Initilize(GraphicsDevice gd)
        {
            graphicsDevice = gd;
            spriteBatch = new SpriteBatch(graphicsDevice);
            Sprites = new List<SpriteRenderer>();
            HUD = new List<SpriteRenderer>();
        }

        public static void Clear()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            Sprites = new List<SpriteRenderer>();
            HUD = new List<SpriteRenderer>();
        }

        public static void AddSpriteToDraw(SpriteRenderer s)
        {
            if (s.IsHUD)
                HUD.Add(s);
            else
                Sprites.Add(s);
        }

        public static void Draw(float gt)
        {

            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            WindowScale = new Vector2(graphicsDevice.Viewport.Width / WIDTH, graphicsDevice.Viewport.Height / HEIGHT);        

            IEnumerable<SpriteRenderer> s = Sprites.OrderBy(s => s.Layer)
                                         .ThenByDescending(s => s.Transform.Position.Y)
                                         .ThenBy(s => s.OrderInLayer);

            foreach (SpriteRenderer sr in s)
            {
                if (sr.Shader != null)
                {
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    sr.Shader.CurrentTechnique.Passes[0].Apply();
                }               

                sr.Posted = false;
                spriteBatch.Draw(SceneManager.CurrentScene.Textures[sr.Texture],
                    (sr.WorldPosition() - Camera.Position + (new Vector2(WIDTH / 2, HEIGHT / 2) * WindowScale)),
                    sr.DrawRect(),
                    new Color(sr.Color.R - (int)GlobalFade, sr.Color.G - (int)GlobalFade, sr.Color.B - (int)GlobalFade, sr.Color.A),
                    sr.Transform.Rotation,
                    new Vector2(sr.Transform.Width / 2, sr.Transform.Height / 2),
                    GameScale * sr.Transform.Scale,
                    SpriteEffects.None,
                    0);

                if (sr.Shader != null)
                {
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                }
            }


            s = HUD.OrderBy(s => s.Layer)
                                .ThenByDescending(s => s.Transform.Position.Y)
                                .ThenBy(s => s.OrderInLayer);

            foreach (SpriteRenderer sr in s)
            {
                spriteBatch.Draw(SceneManager.CurrentScene.Textures[sr.Texture],
                    sr.WorldPosition() + (new Vector2(WIDTH/2, HEIGHT/2) * WindowScale),
                    sr.DrawRect(),
                    new Color(sr.Color.R - (int)GlobalFade, sr.Color.G - (int)GlobalFade, sr.Color.B - (int)GlobalFade, sr.Color.A),
                    sr.Transform.Rotation,
                    new Vector2(0, 0),
                    WindowScale * sr.Transform.Scale,
                    SpriteEffects.None,
                    0);
            }

            spriteBatch.End();

            Sprites.Clear();
        }
    }
}
