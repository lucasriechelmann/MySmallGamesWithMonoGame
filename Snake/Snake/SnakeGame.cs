using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Content;

namespace Snake
{
    public class SnakeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Grid _grid;
        SnakePlayer _snakePlayer;
        SnakeFruit _snakeFruit;
        public SnakeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _grid = new(Content.Load<Texture2D>("snake-grid"), 64, 64, 10, 10);            
            _snakePlayer = new(Content.Load<Texture2D>("snake-head"), Content.Load<Texture2D>("snake-body"), _graphics.GraphicsDevice.Viewport);
            _snakeFruit = new(Content.Load<Texture2D>("snake-fruit"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();            

            _snakePlayer.Update(gameTime, _snakeFruit);
            _snakeFruit.Update(gameTime, _snakePlayer);
            _snakeFruit.Eat(_snakePlayer);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _grid.Draw(_spriteBatch);
            _snakePlayer.Draw(_spriteBatch);
            _snakeFruit.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
