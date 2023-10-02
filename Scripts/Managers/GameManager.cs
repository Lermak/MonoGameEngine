using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;

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
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            Window.Title = Globals.GAME_TITLE;

            RenderingManager.GraphicsDevice = GraphicsDevice;
            TimeManager.Initilize();
            ConfigurationManager.Initilize();
            ResourceManager.Initilize(Content);
            InputManager.Initilize();
            RenderingManager.Initilize();
            SoundManager.Initilize();
            CollisionManager.Initilize();
            CoroutineManager.Initilize();
            CameraManager.Initilize();
            SceneManager.Initilize(new CombatScene());
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }
        public static void Quit()
        {
            quit = true;
        }

        protected override void Update(GameTime deltaTime)
        {
            if (quit)
                Exit();

            TimeManager.Update(deltaTime);

            InputManager.Update(TimeManager.DeltaTime);

            CoroutineManager.Update(TimeManager.ProdDelta);

            SceneManager.Update(TimeManager.ProdDelta);

            CameraManager.Update(TimeManager.ProdDelta);

            CollisionManager.Update();

            base.Update(deltaTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderingManager.Draw(TimeManager.DeltaTime);

            base.Draw(gameTime);
        }
    }
}
