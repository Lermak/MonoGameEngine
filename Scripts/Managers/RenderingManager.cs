using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public static class RenderingManager
    {
        const float WIDTH = 1920;
        const float HEIGHT = 1080;

        public static Vector2 Scale = new Vector2(1,1);
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

        public static void AddSpriteToDraw(SpriteRenderer s)
        {
            if (s.IsHUD)
                HUD.Add(s);
            else
                Sprites.Add(s);
        }

        public static void Draw(GameTime gt)
        {

            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            Scale = new Vector2(graphicsDevice.Viewport.Width / WIDTH, graphicsDevice.Viewport.Height / HEIGHT);

            IEnumerable<SpriteRenderer> s = Sprites.OrderBy(s => s.Layer)
                                         .ThenByDescending(s => s.Transform.Position.Y)
                                         .ThenBy(s => s.OrderInLayer);

            foreach (SpriteRenderer sr in s)//(int i = 0; i < s.Count(); ++i)
            {
                //System.Diagnostics.Debug.WriteLine(GridMap.WorldToGridPosition(sr.WorldPosition(), sr.zOrder));
                sr.Posted = false;
                spriteBatch.Draw(SceneManager.CurrentScene.Textures[sr.Texture], 
                    (sr.WorldPosition() - Camera.Position), 
                    sr.DrawRect(), 
                    sr.Color, 
                    0, 
                    new Vector2(0,0),
                    Scale, 
                    SpriteEffects.None, 
                    0);
            }


            s = Sprites.OrderBy(s => s.Layer)
                                .ThenByDescending(s => s.Transform.Position.Y)
                                .ThenBy(s => s.OrderInLayer);

            foreach (SpriteRenderer sr in s)
            {
                spriteBatch.Draw(SceneManager.CurrentScene.Textures[sr.Texture],
                    sr.WorldPosition() * Scale,
                    sr.DrawRect(),
                    sr.Color,
                    0,
                    new Vector2(0, 0),
                    Scale,
                    SpriteEffects.None,
                    0);
            }

            spriteBatch.End();

            Sprites.Clear();
        }
    }
}
