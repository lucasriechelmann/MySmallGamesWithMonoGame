using AnasStudio.Engine;
using AnasStudio.Engine.Constants;
using AnasStudio.Engine.Inputs;
using AnasStudio.Engine.Managers;
using AnasStudio.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TopGun.GameObjects;

public class Player : AnimatedGameObject
{
    TopDownInput _input;
    GameObjectList _fires;
    float _speed = 200;
    float _shootElapsedTime = 0;
    bool _shoot = false;
    public event Action<Vector2> OnShoot;

    public Player(Vector2 position, TopDownInput input) : base(DephtConstants.PLAYER)
    {
        LocalPosition = position;
        _input = input;
        _fires = new();
        _fires.Parent = this;
        AnimatedGameObject leftFireObject = new(DephtConstants.PLAYER);
        leftFireObject.LoadAnimation("Sprites/aircraft_fire", "firing", true, 0.1F, 0, 3, 1);
        leftFireObject.PlayAnimation("firing");
        AnimatedGameObject rightFireObject = new(DephtConstants.PLAYER);
        rightFireObject.LoadAnimation("Sprites/aircraft_fire", "firing", true, 0.1F, 0, 3, 1);
        rightFireObject.PlayAnimation("firing");
        leftFireObject.LocalPosition = new Vector2(6, 13);
        rightFireObject.LocalPosition = new Vector2(-13, 13);

        _fires.AddChild(leftFireObject);
        _fires.AddChild(rightFireObject);        

        LoadAnimation("Sprites/aircraft", "idle", false, 0.5F);
        PlayAnimation("idle");
        SetOriginToCenter();
        Reset();
    }
    public override void HandleInput(InputManager inputManager)
    {
        base.HandleInput(inputManager);

        _velocity = Vector2.Zero;

        if (inputManager.KeyDown(_input.Up))
            _velocity.Y -= _speed;

        if (inputManager.KeyDown(_input.Down))
            _velocity.Y += _speed;

        if (inputManager.KeyDown(_input.Left))
            _velocity.X -= _speed;

        if (inputManager.KeyDown(_input.Right))
            _velocity.X += _speed;

        if(inputManager.KeyDown(_input.Fire))
            _shoot = true;
    }
    public override void Update(GameTime gameTime)
    {
        _shootElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        base.Update(gameTime);
        _fires.Update(gameTime);

        Shoot(gameTime);

        if (IsOutsideScreen())
            UpdateBack(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _fires.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);        
    }
    void Shoot(GameTime gameTime)
    {
        _shootElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (_shootElapsedTime > 200 && _shoot)
        {
            _shootElapsedTime = 0;
            _shoot = false;
            OnShoot?.Invoke(LocalPosition);
        }
    }
    bool IsOutsideScreen()
    {
        Point _worldSize = ExtendedGame.WorldSize;
        if (LocalPosition.X - Width / 2 < 0)
            return true;

        if (LocalPosition.X + Width / 2 > _worldSize.X)
            return true;

        if (LocalPosition.Y - Height / 2 < 0)
            return true;

        if (LocalPosition.Y + Height / 2 > _worldSize.Y)
            return true;

        return false;
    }
}
