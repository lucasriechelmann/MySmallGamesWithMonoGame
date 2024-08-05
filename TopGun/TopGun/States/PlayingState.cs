using AnasStudio.Engine;
using AnasStudio.Engine.Constants;
using AnasStudio.Engine.Inputs;
using AnasStudio.Engine.Managers;
using AnasStudio.Engine.Objects;
using AnasStudio.Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopGun.GameObjects;
using TopGun.Managers;

namespace TopGun.States;

public class PlayingState : GameState
{
    PlayerManager _playerManager;
    EnemyManager _enemyManager;
    CollisionManager _collisionManager;
    public PlayingState()
    {
        Reset();
    }
    public override void HandleInput(InputManager inputManager)
    {
        base.HandleInput(inputManager);
        _playerManager.HandleInput(inputManager);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _playerManager.Update(gameTime);
        _enemyManager.Update(gameTime);
        _collisionManager.HandleCollisions();

        if(_playerManager.Player.IsRemoved)
        {
            ExtendedGame.GameStateManager.SwitchTo(GameStateConstants.GAME_OVER);            
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        _playerManager.Draw(gameTime, spriteBatch);
        _enemyManager.Draw(gameTime, spriteBatch);
    }
    public override void Reset()
    {
        base.Reset();
        ParallaxBackground parallaxBackground = new("Sprites/sea_with-waves", new(true, ParallaxDirection.TopBottom, DephtConstants.BACKGROUND, 200));
        AddChild(parallaxBackground);
        _playerManager = new();
        _enemyManager = new();
        _collisionManager = new(_playerManager, _enemyManager);
    }
}
