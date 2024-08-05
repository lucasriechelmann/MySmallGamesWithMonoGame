using AnasStudio.Engine;
using AnasStudio.Engine.Inputs;
using AnasStudio.Engine.Managers;
using AnasStudio.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TopGun.GameObjects;

namespace TopGun.Managers;

public class PlayerManager
{
    Player _player;
    GameObjectList _shoots = new();
    public Player Player => _player;
    public GameObjectList Shoots => _shoots;

    public PlayerManager()
    {
        _player = new Player(new(ExtendedGame.WorldSize.X / 2 - 32, ExtendedGame.WorldSize.Y - 128), new TopDownInput()
        {
            Down = Keys.Down,
            Up = Keys.Up,
            Left = Keys.Left,
            Right = Keys.Right
        });
        _player.OnShoot += Shoot;
    }
    public void HandleInput(InputManager inputManager)
    {
        _player.HandleInput(inputManager);
    }
    public void Update(GameTime gameTime)
    {
        
        _player.Update(gameTime);
        _shoots.Update(gameTime);
        _shoots.RemoveNotVisible();
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _shoots.Draw(gameTime, spriteBatch);
        _player.Draw(gameTime, spriteBatch);        
    }
    void Shoot(Vector2 position)
    {
        Shoot shoot = new(-200);
        shoot.LocalPosition = new Vector2(position.X, position.Y - 25);
        _shoots.AddChild(shoot);
    }
}
