using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace FlapSeagull;

public class Pipes
{
    Texture2D _texture;
    Rectangle _pipeTop;
    Rectangle _pipeBottom;
    Rectangle _source;
    Random _random = new Random();
    int _screenWidth;
    int _screenHeight;
    public Pipes(Texture2D texture, int screenWidth, int screenHeight, int positionX)
    {
        _texture = texture;
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;
        _source = new Rectangle(0, 0, _texture.Width, _texture.Height);

        Reset(positionX);
    }

    public void Reset(int? positionX = null)
    {
        int x = positionX.HasValue ? positionX.Value : _screenWidth + _texture.Width;
        int randomY = _random.Next(150, _screenHeight - 150);
        int topY = randomY - _texture.Height;
        int bottomY = randomY + 150;

        _pipeTop = new Rectangle(x, topY, _texture.Width, _texture.Height);
        _pipeBottom = new Rectangle(x, bottomY, _texture.Width, _texture.Height);
    }
    public void Update(GameTime gameTime)
    {
        _pipeTop.X = _pipeTop.X - 3;
        _pipeBottom.X = _pipeBottom.X - 3;

        if (_pipeTop.X + _texture.Width < 0)
        {
            Reset();
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _pipeTop, _source, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
        spriteBatch.Draw(_texture, _pipeBottom, _source, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
    }
    public Rectangle GetTopBounds() => _pipeTop;
    public Rectangle GetBottomBounds() => _pipeBottom;
}
