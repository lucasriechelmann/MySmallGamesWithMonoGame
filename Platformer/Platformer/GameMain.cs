using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Platformer.Components;
using Platformer.Systems;
using System.Collections.Generic;

namespace Platformer;

public class GameMain : GameBase
{
    TiledMap _map;
    TiledMapRenderer _renderer;
    OrthographicCamera _camera;
    World _world;
    EntityFactory _entityFactory;    
    public Size WorldSize { get; private set; }
    public GameMain() : base(1280, 720)
    {
        
    }
    protected override void RegisterDependencies(ContainerBuilder builder)
    {
        _camera = new OrthographicCamera(GraphicsDevice);
        builder.RegisterInstance(this);
        builder.RegisterInstance(new SpriteBatch(GraphicsDevice));
        builder.RegisterInstance(_camera);
        builder.RegisterInstance(GraphicsDevice);
        builder.RegisterInstance(Content);
        builder.RegisterType<PlayerSystem>();
        builder.RegisterType<RenderSystem>();
        builder.RegisterType<CollisionSystem>();
        builder.RegisterType<DebugTileSystem>();
        builder.RegisterType<CameraSystem>();
    }
    protected override void LoadContent()
    {
        _world = new WorldBuilder()
            .AddSystem(Container.Resolve<PlayerSystem>())
            .AddSystem(Container.Resolve<CollisionSystem>())
            .AddSystem(Container.Resolve<CameraSystem>())
            .AddSystem(Container.Resolve<RenderSystem>())
            //.AddSystem(Container.Resolve<DebugTileSystem>())
            .Build();
        Components.Add(_world);

        _map = Content.Load<TiledMap>("Tiled/Map/level1");
        _renderer = new TiledMapRenderer(GraphicsDevice, _map);
        WorldSize = new(_map.WidthInPixels, _map.HeightInPixels);

        _entityFactory = new EntityFactory(_world, Content);

        _entityFactory.CreatePlayer(new Vector2(32, 32));

        CreateCollisionBodies();
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardExtended.Update();

        _renderer.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _renderer.Draw(0, _camera.GetViewMatrix());

        base.Draw(gameTime);
    }
    void CreateCollisionBodies()
    {
        int tileWidth = _map.TileWidth;
        int tileHeight = _map.TileHeight;
        var layer = _map.GetLayer<TiledMapTileLayer>("collision");
        foreach(var tile in layer.Tiles)
        {
            _entityFactory.CreateTile(tile.X, tile.Y, tileWidth, tileHeight);
        }
    }
}
