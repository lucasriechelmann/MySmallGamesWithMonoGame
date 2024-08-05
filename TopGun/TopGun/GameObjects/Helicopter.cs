using AnasStudio.Engine;
using AnasStudio.Engine.Constants;
using AnasStudio.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TopGun.GameObjects;

public class Helicopter : SpriteGameObject
{
    SpriteSheet _spriteSheet;
    float _rotationSpeed = 100;
    float _rotation = 0;
    public event Action<Vector2> OnShoot;
    float _shootCooldown = 0;
    float _shootElapsed = 0;
    float _shootCount = 0;
    Rectangle _worldLimit;
    public Helicopter() : base("Sprites/helicopter", DephtConstants.ENEMY, 0, 2, 1)
    {
        _spriteSheet = new SpriteSheet("Sprites/helicopter", DephtConstants.ENEMY + 0.05F, 1, 2, 1);
        _velocity.Y = 100;
        SetOriginToCenter();
        _worldLimit = new(0, 0, ExtendedGame.WorldSize.X, ExtendedGame.WorldSize.Y);
        
    }
    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        
        base.Update(gameTime);

        Shoot(gameTime, deltaTime);                

        if (GlobalPosition.Y - Height / 2 > ExtendedGame.WorldSize.Y)
            Visible = false;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        _rotation += _rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        _spriteSheet.Draw(spriteBatch, LocalPosition, new Vector2(32,32), _rotation);
    }
    void Shoot(GameTime gameTime, float deltaTime)
    {
        if (_worldLimit.Intersects(BoundingBox))
        {
            _shootCooldown += deltaTime;
            if (_shootCooldown > 2000)
            {
                _shootElapsed += deltaTime;
                if (_shootElapsed > 200)
                {
                    _shootElapsed = 0;
                    OnShoot?.Invoke(GlobalPosition);
                    _shootCount++;
                }

                if (_shootCount >= 3)
                {
                    _shootCooldown = 0;
                    _shootCount = 0;
                }
            }
        }
    }
    public override Rectangle BoundingBox
    {
        get
        {
            // get the sprite's bounds
            Rectangle spriteBounds = _sprite.Bounds;
            spriteBounds.X = spriteBounds.Width / 4;
            spriteBounds.Width = spriteBounds.Width / 2;
            spriteBounds.Y += 2;
            spriteBounds.Height -= 10;
            // add the object's position to it as an offset
            spriteBounds.Offset(GlobalPosition - Origin);
            return spriteBounds;
        }
    }
}
