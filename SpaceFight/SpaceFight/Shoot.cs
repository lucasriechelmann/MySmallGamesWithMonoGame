using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFight;

public class Shoot : GameObject
{
    public Shoot(Texture2D texture, Vector2 position, Viewport viewport) : base(texture, position, viewport)
    {
        _yVelocity = -5;
        _velocity = new(_xVelocity, _yVelocity);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if(_position.Y < 0)
            IsRemoved = true;
    }
}
