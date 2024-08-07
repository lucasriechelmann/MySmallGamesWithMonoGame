using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using Platformer.Objects;
using System.Collections.Generic;

namespace Platformer.Managers;

public class LevelManager
{
    GameMain _game;
    OrthographicCamera _camera;
    World _world;
    public LevelManager(GameMain game, OrthographicCamera camera, World world)
    {
        _game = game;
        _camera = camera;
        _world = world;
    }
    Dictionary<string, Level> _levels = new();
    Level _currentLevel;
    public void AddLevel(string levelId, string mapPath, params int[] layers)
    {
        _levels[levelId] = new Level(_game, mapPath, _camera, _world, layers);
    }
    public void ChangeLevel(string levelId)
    {
        _currentLevel?.UnloadContent();
        _currentLevel = _levels[levelId];
        _currentLevel.LoadContent();
    }
    public void Update(GameTime gameTime)
    {
        _currentLevel?.Update(gameTime);
    }
    public void Draw(GameTime gameTime)
    {
        _currentLevel?.Draw(gameTime);
    }
}
