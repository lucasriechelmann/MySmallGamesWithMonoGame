using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFight;

public class  Enemy : GameObject
{    
    public Enemy(Texture2D texture, Vector2 position, float yVelocity, Viewport viewport) : base(texture, position, viewport)
    {
        _yVelocity = yVelocity;
        _velocity = new(_xVelocity, _yVelocity);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if(_position.Y > _viewport.Height)
            IsRemoved = true;
    }
}
