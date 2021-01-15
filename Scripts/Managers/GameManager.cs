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
            RenderingManager.Initilize(GraphicsDevice);
            SoundManager.Initilize();
            CollisionManager.Initilize();
            SceneManager.Initilize(Content);
            Camera.Initilize();
            SceneManager.ChangeScene(new TestScene());
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

            // TODO: Add your update logic here
            SceneManager.Update(gameTime);
            Cursor.Update(gameTime);
            Camera.Update(gameTime);
            CollisionManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderingManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
