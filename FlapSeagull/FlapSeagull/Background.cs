using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlapSeagull;

public class Background
{
    Texture2D _texture;
    Rectangle _sourceRectangle;
    Rectangle _destinationRectangle;
    public Background(Texture2D texture, int screenWidth, int screenHeight)
    {
        _texture = texture;
        _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
        _destinationRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _destinationRectangle, _sourceRectangle, Color.White);
    }
}
