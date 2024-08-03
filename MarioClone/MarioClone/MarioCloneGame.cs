using MarioClone.Components;
using MarioClone.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace MarioClone
{
    public class MarioCloneGame : Game
    {
        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        World _world;
        OrthographicCamera _camera;
        Viewport _viewport;
        public MarioCloneGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _viewport = GraphicsDevice.Viewport;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, SCREEN_WIDTH, SCREEN_HEIGHT);
            _camera = new OrthographicCamera(viewportAdapter);
            _tiledMap = Content.Load<TiledMap>("level1"); // Load your .tmx file
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _world = new WorldBuilder()
                .AddSystem(new InputSystem())
                .AddSystem(new MovementSystem())
                .AddSystem(new CameraFollowSystem(_camera))
                .AddSystem(new TiledMapRenderSystem(_tiledMapRenderer, _camera, _spriteBatch))                
                .AddSystem(new RenderSystem(_spriteBatch, _camera))
                .Build();

            Texture2D texture = Content.Load<Texture2D>("mario");

            Entity entity = _world.CreateEntity();
            entity.Attach(new TransformComponent { Position = new Vector2(100, 100), Scale = Vector2.One * 2 });
            entity.Attach(new VelocityComponent { Velocity = new Vector2(100, 100) });
            //entity.Attach(new SpriteComponent(texture, new Rectangle(0, 0, 32, 32)));
            entity.Attach(new PlayerAnimationComponent(texture));
            entity.Attach(new InputComponent());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _world.Draw(gameTime);            

            base.Draw(gameTime);
        }
    }
}
