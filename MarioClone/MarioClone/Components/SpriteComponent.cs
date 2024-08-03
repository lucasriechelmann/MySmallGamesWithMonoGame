using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Components;

public class SpriteComponent
{
    public Texture2D Texture { get; set; }
    public Rectangle SourceRectangle { get; set; }

    public SpriteComponent(Texture2D texture, Rectangle sourceRectangle)
    {
        Texture = texture;
        SourceRectangle = sourceRectangle;
    }
}
