using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FlapSeagull;

public class Seagull
{
    Viewport _viewport;
    Vector2 _position;
    float _speedUp = 5;
    float _speedDown = 3;
    Texture2D _texture;
    Texture2D _boundBoxTexture;
    List<Rectangle> _frames;
    int _currentFrame;
    int _frameWidth;
    int _frameHeight;
    int _frameTime;
    float _timeElapsed;
    int _scale = 3;
    bool _isAlive = true;
    public bool IsAlive => _isAlive;
    public Seagull(Texture2D texture, Vector2 position, int width, int heigth, int frameQuantity, int frameTime, Viewport viewport)
    {
        
        _texture = texture;
        _frameWidth = width;
        _frameHeight = heigth;
        _frames = new List<Rectangle>();
        _currentFrame = 0;
        _frameTime = frameTime;
        _position = position;
        _viewport = viewport;
        _boundBoxTexture = new Texture2D(_texture.GraphicsDevice, 1, 1);
        _boundBoxTexture.SetData(new Color[] { Color.White });

        for (int i = 0; i < frameQuantity; i++)
        {
            Rectangle frame = new Rectangle(i * _frameWidth, 0, _frameWidth, _frameHeight);
            _frames.Add(frame);
        }
    }
    public void Reset()
    {
        _position = new Vector2(100, _viewport.Height / 2 - _frameHeight / 2);
        _isAlive = true;
    }
    public void Die()
    {
        _isAlive = false;
    }
    public void Update(GameTime gameTime)
    {
        if(!_isAlive)
        {
            return;
        }

        _timeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (_timeElapsed > _frameTime)
        {
            _currentFrame++;
            if (_currentFrame >= _frames.Count)
            {
                _currentFrame = 0;
            }
            _timeElapsed = 0;
        }

        KeyboardState state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Space))
        {
            _position.Y -= _speedUp;
        }
        else
        {
            _position.Y += _speedDown;
        }

        if (_position.Y < 0)
        {
            _position.Y = 0;
        }

        if (_position.Y + (_frameHeight * _scale) > _viewport.Height)
        {
            _position.Y = _viewport.Height - (_frameHeight * _scale);
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(_boundBoxTexture, GetBounds(), Color.Red);
        spriteBatch.Draw(_texture, _position, _frames[_currentFrame], Color.White, 0, Vector2.Zero, _scale, SpriteEffects.FlipHorizontally, 0);
    }
    public Rectangle GetBounds()
    {
        return new(
            (int)_position.X + 10, 
            (int)_position.Y + 35, 
            (_frameWidth - 8) * _scale,
            (_frameHeight - 22) * _scale);
    }
}
