using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.ECS;
using System.Collections.Generic;

namespace Platformer.Objects;

public class Level : LevelBase
{
    public Level(GameMain game, string mapPath, OrthographicCamera camera, World world, params int[] layers) : 
        base(game, mapPath, camera, world, layers)
    {
        
    }
    protected override void Load()
    {
        
    }
    public override void UnloadContent()
    {
        
    }
}
public abstract class LevelBase
{
    List<Entity> _entities = new();
    World _world;
    protected string _mapPath;
    protected GameMain _game;
    protected OrthographicCamera _camera;
    protected EntityFactory _entityFactory;
    protected int[] _layers;
    protected TiledMap _map;
    protected TiledMapRenderer _renderer;
    protected ContentManager _content => _game.Content;
    protected GraphicsDevice _graphicsDevice => _game.GraphicsDevice;
    public LevelBase(GameMain game, string mapPath, OrthographicCamera camera, World world, params int[] layers)
    {
        _world = world;
        _game = game;
        _mapPath = mapPath;
        _camera = camera;
        _entityFactory = new(_world, _game.Content);
        _layers = layers;
    }

    public virtual void LoadContent()
    {
        if (_map is null)
        {
            _map = _game.Content.Load<TiledMap>(_mapPath);
            _renderer = new TiledMapRenderer(_game.GraphicsDevice, _map);
            Entity player = _entityFactory.CreatePlayer(new(32,32));
        }

        _game.WorldSize = new Size(_map.WidthInPixels, _map.HeightInPixels);

        CreateCollisionBodies();

        Load();
    }
    public virtual void UnloadContent()
    {
        foreach(Entity entity in _entities)
            _world.DestroyEntity(entity.Id);

        _entities.Clear();
    }
    public virtual void Update(GameTime gameTime)
    {
        _renderer.Update(gameTime);
    }
    public virtual void Draw(GameTime gameTime)
    {
        for (int i = 0; i < _layers.Length; i++)
            _renderer.Draw(_layers[i], _camera.GetViewMatrix());
    }
    protected abstract void Load();
    void CreateCollisionBodies()
    {
        int tileWidth = _map.TileWidth;
        int tileHeight = _map.TileHeight;
        var layer = _map.GetLayer<TiledMapTileLayer>("collision");
        foreach (var tile in layer.Tiles)
        {
            Entity entity = _entityFactory.CreateTile(tile.X, tile.Y, tileWidth, tileHeight);
            _entities.Add(entity);
        }
    }
}
