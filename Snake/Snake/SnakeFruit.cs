using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Snake;

public class SnakeFruit
{
    Texture2D _texture;
    Vector2 _position;
    Vector2 _origin;
    bool _isEaten = true;
    Random _random = new();
    float _elapsedTime;
    public SnakeFruit(Texture2D texture)
    {
        _texture = texture;        
        _origin = new(_texture.Width / 2, _texture.Height / 2);
        _elapsedTime = 0;
    }    
    public void Update(GameTime gameTime, SnakePlayer snake)
    {
        if (_isEaten)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (_elapsedTime >= 1000)
            {                
                _elapsedTime = 0;
                GeneratePosition(snake);
            }            
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if(!_isEaten)
            spriteBatch.Draw(_texture, _position, null, Color.White, 0, _origin, 1, SpriteEffects.None, 0);
    }
    public void Eat(SnakePlayer snake)
    {
        if (snake.GetHeadBounds().Intersects(GetBounds()) && !_isEaten)
        {
            _isEaten = true;
            snake.AddBody();
        }
    }
    void GeneratePosition(SnakePlayer snake)
    {
        bool isValid = false;

        while (!isValid)
        {
            _position = new((_random.Next(0, 10) * 64) + 32, (_random.Next(0, 10) * 64) + 32);

            Rectangle fruitBounds = GetBounds();

            if (!snake.GetHeadBounds().Intersects(fruitBounds) && !snake.GetBodyBounds().Any(x => x.Intersects(fruitBounds)))
                isValid = true;

            _isEaten = false;
        }
    }
    public Rectangle GetBounds() => new((int)_position.X - 32, (int)_position.Y - 32, _texture.Width, _texture.Height);
    public void Reset() => _isEaten = true;
}
