using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceFight;

public abstract class GameObject
{
    protected Texture2D _texture;
    protected Vector2 _position;
    protected Vector2 _velocity;
    protected float _xVelocity = 0;
    protected float _yVelocity = 0;
    protected float _elapsedTime = 0;
    protected Random _random = new();
    protected Viewport _viewport;
    public bool IsRemoved { get; set; }
    public Vector2 Position => _position;
    protected GameObject(Texture2D texture, Vector2 position, Viewport viewport)
    {
        _texture = texture;
        _position = position;
        _velocity = new(_xVelocity, _yVelocity);
        IsRemoved = false;
        _viewport = viewport;
    }
    public virtual void Reset()
    {
        IsRemoved = false;
    }
    public void Remove() => IsRemoved = true;
    public void SetPosition(Vector2 position) => _position = position;
    public virtual void Update(GameTime gameTime)
    {
        if(IsRemoved)
            return;

        _position += _velocity;
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if(IsRemoved)
            return;

        spriteBatch.Draw(_texture, _position, Color.White);
    }
    public Rectangle GetBounds() => new(new((int)_position.X, (int)_position.Y), new(_texture.Width, _texture.Height));
}
