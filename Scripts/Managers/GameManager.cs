using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public class GameManager : Game
    {
        private GraphicsDeviceManager _graphics;

        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            InputManager.Initilize();
            RenderingManager.Initilize(GraphicsDevice);
            SoundManager.Initilize();
            CollisionManager.Initilize();
            SceneManager.Initilize(Content);
            Camera.Initilize();
            SceneManager.ChangeScene(new MainMenu());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float gt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            InputManager.Update(gt);
            SceneManager.Update(gt);
            Cursor.Update(gt);
            Camera.Update(gt);
            CollisionManager.Update(gt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float gt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            RenderingManager.Draw(gt);

            base.Draw(gameTime);
        }
    }
}
