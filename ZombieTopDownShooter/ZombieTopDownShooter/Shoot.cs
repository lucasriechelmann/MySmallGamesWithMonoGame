using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieTopDownShooter
{
    public class Shoot
    {
        Texture2D _texture;
        Vector2 _position;
        Vector2 _direction;
        Vector2 _origin;
        float _rotation;
        float _speed = 400;
        bool _isAlive = true;
        Rectangle _source;
        Rectangle _destination;
        Viewport _viewport;
        public bool IsAlive => _isAlive;
        public Shoot(Texture2D texture, Viewport viewport)
        {
            _texture = texture;
            _direction = Vector2.Zero;
            _source = new(64, 0, 4, 3);
            _origin = new(2, 1.5f);
            _rotation = 0;
            _viewport = viewport;
        }
        public void Fire(Vector2 position, Vector2 direction, float rotation)
        {
            _position = position;
            _direction = direction;
            _rotation = rotation;
            _isAlive = true;
        }
        public void Update(GameTime gameTime)
        {
            if(!_isAlive)
            {
                return;
            }
            _position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _destination = new((int)_position.X, (int)_position.Y, 14, 13);

            if (_position.X + _destination.Width < 0 || _position.X > _viewport.Width || _position.Y + _destination.Height < 0 || _position.Y > _viewport.Height)
            {
                _isAlive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isAlive)
            {
                return;
            }
            spriteBatch.Draw(_texture, _destination, _source, Color.White, _rotation, _origin, SpriteEffects.None, 0);
        }
        public void Destroy()
        {
            _isAlive = false;
        }
        public Rectangle Bounds => _destination;
    }
}
