using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlapSeagull;

public class Cloud
{
    Viewport _viewport;
    Texture2D _texture;
    Vector2 _position;
    Vector2 _velocity;
    float _scale;
    Random _random = new Random();
    public Cloud(Viewport viewport, Texture2D texture, Vector2 position)
    {
        _viewport = viewport;
        _texture = texture;
        _position = position;
        _velocity = new Vector2(-5, 0);
        _scale = GetScale();
    }
    float GetScale()
    {
        int value = _random.Next(10, 50);
        return value / 100f;
    }
    void Reset()
    {
        _scale = GetScale();
        int height = (int)(_texture.Height * _scale);
        float y = _random.Next(0, _viewport.Height - height);
        _position = new Vector2(_viewport.Width + (_texture.Width * _scale), y);
    }
    public void Update(GameTime gameTime)
    {
        _position += _velocity;

        if (_position.X + (_texture.Width * _scale) < 0)
        {
            Reset();
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);
    }
}
