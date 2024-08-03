using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ZombieTopDownShooter;

public class Player
{
    Texture2D _texture;
    Vector2 _position;
    float _rotation;
    float _speed = 200;
    Rectangle _source;
    Rectangle _destination;
    Vector2 _origin;
    bool _isAlive = true;
    Vector2 _startPosition;
    List<Shoot> _shoots;
    public Vector2 Position => _position;
    public bool IsAlive => _isAlive;
    float _elapsedTime;
    Viewport _viewport;
    public Player(Texture2D texture, Vector2 position, List<Shoot> shoots, Viewport viewport)
    {
        _texture = texture;
        _startPosition = position;
        _position = position;
        _source = new(10, 8, 17, 15);
        _origin = new(8, 7);
        _shoots = shoots;
        _elapsedTime = 0;
        _viewport = viewport;
    }
    public void Update(GameTime gameTime)
    {               
        _destination = new((int)_position.X, (int)_position.Y, 64, 64);
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = mouseState.Position.ToVector2();
        Vector2 direction = mousePosition - _position;

        if (direction != Vector2.Zero)
            direction.Normalize();

        _rotation = (float)Math.Atan2(direction.Y, direction.X);
        HandleInput(gameTime, direction, mouseState);
    }
    void HandleInput(GameTime gameTime, Vector2 direction, MouseState mouseState)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState keyboardState = Keyboard.GetState();        

        Vector2 moveDirection = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W))
        {
            //moveDirection += direction;
            moveDirection.Y -= _speed;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            //moveDirection -= direction;
            moveDirection.Y += _speed;
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            //Vector2 left = new(direction.Y, -direction.X);
            //moveDirection += left;
            moveDirection.X -= _speed;
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            //Vector2 right = new(-direction.Y, direction.X); 
            //moveDirection += right;
            moveDirection.X += _speed;
        }

        //if (moveDirection != Vector2.Zero)
        //    moveDirection.Normalize();

        //_position += moveDirection * _speed * deltaTime;
        _position += moveDirection * deltaTime;

        _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (_elapsedTime > 200)
        {            
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                _elapsedTime = 0;
                Shoot shoot = _shoots.Find(s => !s.IsAlive);

                if(shoot is null)
                {
                    shoot = new(_texture, _viewport);
                    _shoots.Add(shoot);
                }

                Vector2 gunOffset = new Vector2(_destination.Width / 2, 0); // Adjust this offset based on the gun's position relative to the sprite center
                Vector2 rotatedOffset = Vector2.Transform(gunOffset, Matrix.CreateRotationZ(_rotation));
                Vector2 bulletStartPosition = _position + rotatedOffset;

                shoot.Fire(bulletStartPosition, direction, _rotation);
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture,
            _destination,
            _source,
            Color.White,
            _rotation,
            _origin,
            SpriteEffects.None,
            0f);
    }
    public void Die()
    {
        _isAlive = false;
    }
    public void Reset()
    {
        _position = _startPosition;
        _isAlive = true;
    }
    public Rectangle Bounds => _destination;
}
