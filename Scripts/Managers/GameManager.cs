using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public class GameManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private static bool quit;
        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
        }

        protected override void Initialize()
        {
            RenderingManager.GraphicsDevice = GraphicsDevice;
            // TODO: Add your initialization logic here
            ResourceManager.Initilize(Content);
            InputManager.Initilize();
            RenderingManager.Initilize();
            SoundManager.Initilize();
            CollisionManager.Initilize();
            CoroutineManager.Initilize();
            CameraManager.Initilize();
            SceneManager.Initilize(new SlimeScene());
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }
        public static void Quit()
        {
            quit = true;
        }

        protected override void Update(GameTime deltaTime)
        {
            if (quit)
                Exit();

            // TODO: Add your update logic here
            TimeManager.Update(deltaTime);

            InputManager.Update(TimeManager.DeltaTime);

            CoroutineManager.Update(TimeManager.DeltaTime);

            SceneManager.Update(TimeManager.DeltaTime);

            CameraManager.Update(TimeManager.DeltaTime);

            CollisionManager.Update(TimeManager.DeltaTime);

            base.Update(deltaTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderingManager.Draw(TimeManager.DeltaTime);

            base.Draw(gameTime);
        }
    }
}
