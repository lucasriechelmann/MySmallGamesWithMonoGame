using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceFight;

public class Player : GameObject
{
    public event Action<Vector2> Shoot;
    public Player(Texture2D texture, Vector2 position, Viewport viewport) : base(texture, position, viewport)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;
        KeyboardState keyboardState = Keyboard.GetState();

        _velocity.X = 0;

        if(keyboardState.IsKeyDown(Keys.Left))
            _velocity.X -= 4;

        if(keyboardState.IsKeyDown(Keys.Right))
            _velocity.X += 4;

        if(keyboardState.IsKeyDown(Keys.Space) && _elapsedTime > 250)
        {
            _elapsedTime = 0;
            Vector2 position = GetShootPosition();

            Shoot?.Invoke(position);
        }

        base.Update(gameTime);
    }
    Vector2 GetShootPosition()
    {
        Rectangle bounds = GetBounds();
        return new(_position.X + bounds.Width / 2, _position.Y - 10);
    }
}
