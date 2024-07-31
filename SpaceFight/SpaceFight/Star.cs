using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceFight;

public class Star : GameObject
{
    public Star(Texture2D texture, Vector2 position, Viewport viewport) : base(texture, position, viewport)
    {
        _yVelocity = 1.5f;
        _xVelocity = _random.Next(50, 200) / 100;
        _velocity = new(_xVelocity, _yVelocity);
    }

    public override void Update(GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;

        if (_elapsedTime > 1000)
        {
            _elapsedTime = 0;
            _velocity.X *= -1;
        }
                
        base.Update(gameTime);

        Start();
    }
    public void Start()
    {
        if(_position.Y > _viewport.Height)
            _position.Y = -3;        
    }
}