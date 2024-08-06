using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ECS;
using Platformer.Systems;
using Autofac;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using static Platformer.GameMain;

namespace Platformer.Screens;

public class GameplayScreen : BaseGameScreen
{
    TiledMap _map;
    TiledMapRenderer _renderer;
    OrthographicCamera _camera;
    public GameplayScreen(GameMain game, OrthographicCamera camera) : base(game)
    {
        _camera = camera;
    }

    public override void LoadContent()
    {
        if(_world is null)
        {
            _world = new WorldBuilder()
                .AddSystem(GameMain.Container.Resolve<PlayerSystem>())
                .AddSystem(GameMain.Container.Resolve<CollisionSystem>())
                .AddSystem(GameMain.Container.Resolve<CameraSystem>())
                .AddSystem(GameMain.Container.Resolve<RenderSystem>())
                //.AddSystem(Container.Resolve<DebugTileSystem>())
                .Build();

            _map = Content.Load<TiledMap>("Tiled/Map/level1");
            _renderer = new TiledMapRenderer(GraphicsDevice, _map);

            GameMain.WorldSize = new Size(_map.WidthInPixels, _map.HeightInPixels);

            _entityFactory = new EntityFactory(_world, Content);

            _entityFactory.CreatePlayer(new Vector2(32, 32));

            CreateCollisionBodies();
        }
    }
    public override void Update(GameTime gameTime)
    {
        if (KeyboardExtended.GetState().IsKeyDown(Keys.F1))
        {
            GameMain.LoadScreen(GameState.MainMenu);
            return;
        }

        _renderer.Update(gameTime);
        _world.Update(gameTime);
    }
    public override void Draw(GameTime gameTime)
    {
        _renderer.Draw(0, _camera.GetViewMatrix());
        _world.Draw(gameTime);
    }
    void CreateCollisionBodies()
    {
        int tileWidth = _map.TileWidth;
        int tileHeight = _map.TileHeight;
        var layer = _map.GetLayer<TiledMapTileLayer>("collision");
        foreach (var tile in layer.Tiles)
        {
            _entityFactory.CreateTile(tile.X, tile.Y, tileWidth, tileHeight);
        }
    }
}