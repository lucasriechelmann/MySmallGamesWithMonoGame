using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZombieTopDownShooter;

public class Zombie
{
    Texture2D _texture;
    Texture2D _debugTexture;
    Vector2 _position;
    float _rotation;
    float _speed = 50;
    Rectangle _source;
    Rectangle _destination;
    Vector2 _origin;
    bool _isAlive = true;
    Viewport _viewport;
    public bool IsAlive => _isAlive;
    public Zombie(Texture2D texture, Vector2 position, Viewport viewport, GraphicsDevice graphicsDevice)
    {
        _texture = texture;
        _position = position;
        _source = new(43, 9, 13, 14);
        _origin = new(6, 7);
        _viewport = viewport;
        _debugTexture = new Texture2D(graphicsDevice, 1, 1);
    }
    public void Update(GameTime gameTime, Player player)
    {
        if (!_isAlive)
            return;

        _destination = new((int)_position.X, (int)_position.Y, 64, 64);
        Vector2 direction = player.Position - _position;

        if (direction != Vector2.Zero)
            direction.Normalize();

        _rotation = (float)Math.Atan2(direction.Y, direction.X);

        _position += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        DrawDebug(spriteBatch);

        if (_isAlive)
            spriteBatch.Draw(_texture, _destination, _source, Color.White, _rotation, _origin, SpriteEffects.None, 0);
    }
    public void Die()
    {
        _isAlive = false;
    }
    public void Respawn(Vector2 position)
    {         
        _isAlive = true;
        _position = position;
    }
    public Rectangle Bounds => _destination;
    void DrawDebug(SpriteBatch spriteBatch)
    {
        if (!_isAlive)
            return;

        spriteBatch.Draw(_debugTexture, _destination, Color.Red);
    }
}
