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
        public static GameManager Game;

        private GraphicsDeviceManager _graphics;
        private static bool quit;
        public GameManager()
        {
            Game = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            Window.Title = Globals.GAME_TITLE;

            TimeManager.Initilize();
            ConfigurationManager.Initilize();
            InputManager.Initilize();
            SceneManager.Initilize(new MainMenu());
            
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
            SceneManager.Update(TimeManager.ProdDelta);
            InputManager.Update(TimeManager.DeltaTime);

            base.Update(deltaTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (SceneManager.CurrentScene != null)
            {
                SceneManager.CurrentScene.Draw();
            }
            base.Draw(gameTime);
        }
    }
}
