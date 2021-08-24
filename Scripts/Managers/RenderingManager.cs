using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;


namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Handles the orginization and rendering of all spriteRenders to their sources
    /// </summary>
    public static class RenderingManager
    {
        /// <summary>
        /// Rendering order determines how items will be layered and orgized
        /// </summary>
        public enum RenderOrder { SideScrolling, TopDown, Isometric }
        /// <summary>
        /// Width of the target BackBuffer
        /// </summary>
        public const float WIDTH = 1920;
        /// <summary>
        /// Heigh of the target BackBuffer
        /// </summary>
        public const float HEIGHT = 1080;

        /// <summary>
        /// The global scale of the game after adjusting for the window size
        /// </summary>
        public static Vector2 GameScale { get { return WindowScale * BaseScale; } }
        /// <summary>
        /// The global scale of the game, before adjusting for window size
        /// </summary>
        public static Vector2 BaseScale = new Vector2(1, 1);
        /// <summary>
        /// The scale of the window compaired to the target size
        /// </summary>
        public static Vector2 WindowScale = new Vector2(1, 1);
        /// <summary>
        /// Global color value applied to produce a fade effect between scenes
        /// </summary>
        public static float GlobalFade = 255;
        /// <summary>
        /// Set the rendering order
        /// </summary>
        public static RenderOrder RenderingOrder = RenderOrder.SideScrolling;

        /// <summary>
        /// Render targets are what Cameras use to store image data
        /// </summary>
        public static List<RenderTarget2D> RenderTargets;
        /// <summary>
        /// The list of all game sprites
        /// </summary>
        public static List<SpriteRenderer> Sprites;
        /// <summary>
        /// The batch to use for sending clustered sprite data to render targets
        /// **NOTE** consider using multiple to allow for multiple processes
        /// </summary>
        private static SpriteBatch spriteBatch;
        /// <summary>
        /// MonoGame's object for handling communication with the graphics card
        /// </summary>
        private static GraphicsDevice graphicsDevice;
        

        /// <summary>
        /// Setup the current state of the rendering manager.
        /// This includes creating any render targets that cameras will need
        /// </summary>
        /// <param name="gd">Game Time</param>
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

        /// <summary>
        /// Remove all items from the list of sprites
        /// reset the spriteBatch
        /// </summary>
        public static void Clear()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            Sprites = new List<SpriteRenderer>();
        }

        /// <summary>
        /// Update the window scale to reperesent the current window size
        /// Set the target, then sort all items by their camera
        /// Render all items to their designated targets
        /// Draw all cameras
        /// </summary>
        /// <param name="gt"></param>
        public static void Draw(float gt)
        {
            var x = graphicsDevice.GetRenderTargets();
            WindowScale = new Vector2(graphicsDevice.Viewport.Width / WIDTH, graphicsDevice.Viewport.Height / HEIGHT);

            string prevShader = "";
            int Target = -1;

            SetTarget(Target);

            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
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
                                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                            }

                            prevShader = sr.Shader;
                        }

                        if (c.Target != Target)
                        {
                            spriteBatch.End();

                            SetTarget(c.Target);
                            graphicsDevice.Clear(Color.Transparent);

                            Target = c.Target;

                            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            CameraManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        /// <summary>
        /// Sort the sprite list based on the current sort type
        /// </summary>
        public static void Sort()
        {
            IEnumerable<Camera> cameras = CameraManager.Cameras.OrderByDescending(s => s.Target);

            IEnumerable<SpriteRenderer> s = Sprites;

            foreach (Camera c in cameras)
            {
                if (RenderingOrder == RenderOrder.SideScrolling)
                    s = Sprites.OrderBy(s => s.Shader)
                                .ThenBy(s => s.Transform.Layer)
                                .ThenBy(s => s.OrderInLayer)
                                .Where(s => s.Cameras.Contains(c))
                                .Where(s => Vector2.Distance(s.Transform.Position, c.Transform.Position) <= s.Transform.Radius + c.Transform.Radius);
                else if (RenderingOrder == RenderOrder.TopDown)
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

        /// <summary>
        /// Changes the current Render Target
        /// </summary>
        /// <param name="Target">new target id</param>
        private static void SetTarget(int Target)
        {
            if (Target == -1)
                graphicsDevice.SetRenderTarget(null);
            else
                graphicsDevice.SetRenderTarget(RenderTargets[Target]);
        }
    }
}
