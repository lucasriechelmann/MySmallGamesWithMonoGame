using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Snake;

public class SnakePlayer
{    
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    Texture2D _textureHead;
    Rectangle _headRectangle;
    Texture2D _textureBody;
    Vector2 _headPosition;
    Vector2 _headPreviousPosition;
    Vector2 _snakeOrigin;
    List<Vector2> _bodyPositions;
    Vector2 _velocity;
    Direction _direction;
    float _elapsedTime;
    KeyboardState _keyboardStatePrevious;
    KeyboardState _keyboardStateCurrent;
    const int SPEED = 500;
    int _currentSpeed = SPEED;
    float _rotation;
    Viewport _viewport;
    bool _isAlive = true;
    Color _color;
    bool _isThereNewBody = false;
    public bool IsAlive => _isAlive;
    public SnakePlayer(Texture2D textureHead, Texture2D textureBody, Viewport viewport)
    {
        _textureHead = textureHead;
        _headRectangle = new(0, 0, _textureHead.Width, _textureHead.Height);
        _textureBody = textureBody;
        _headPosition = new(64 + 32, 32);
        _bodyPositions = new() { new Vector2(32, 32)};
        _direction = Direction.Right;        
        _rotation = 0;
        _snakeOrigin = new(_textureHead.Width / 2, _textureHead.Height / 2);
        _viewport = viewport;
        _color = Color.White;        
    }
    public void Update(GameTime gameTime, SnakeFruit fruit)
    {
        _keyboardStatePrevious = _keyboardStateCurrent;
        _keyboardStateCurrent = Keyboard.GetState();
        _velocity = Vector2.Zero;

        if (!_isAlive)
        {
            if(_keyboardStateCurrent.IsKeyDown(Keys.Space) && _keyboardStatePrevious.IsKeyUp(Keys.Space))
            {
                Reset();
                fruit.Reset();
            }
            return;
        }

        SetDirection();
        _elapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;

        if(_elapsedTime >= _currentSpeed)
        {            
            switch (_direction)
            {
                case Direction.Up:
                    _velocity.Y -= 64;
                    _rotation = MathHelper.ToRadians(270);
                    break;
                case Direction.Down:
                    _velocity.Y += 64;
                    _rotation = MathHelper.ToRadians(90);
                    break;
                case Direction.Left:
                    _velocity.X -= 64;
                    _rotation = MathHelper.ToRadians(180);
                    break;
                case Direction.Right:
                    _velocity.X += 64;
                    _rotation = 0;
                    break;
            }
            _elapsedTime = 0;
            _headPreviousPosition = _headPosition;
            UpdateBodyPosition();
        }
        
        _headPosition += _velocity;        
        CheckCollision();        
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_textureHead, _headPosition, _headRectangle, _color, _rotation, _snakeOrigin, 1, SpriteEffects.None, 0);

        foreach (Vector2 position in _bodyPositions)
        {
            spriteBatch.Draw(_textureBody, position, null, _color, 0, _snakeOrigin, 1, SpriteEffects.None, 0);
        }
    }
    public void AddBody()
    {
        _isThereNewBody = true;
        _currentSpeed -= 5;
    }
    void UpdateBodyPosition()
    {
        Vector2 previousPosition = _headPreviousPosition;
        for(int i = 0; i < _bodyPositions.Count; i++)
        {
            Vector2 temp = _bodyPositions[i];
            _bodyPositions[i] = previousPosition;
            previousPosition = temp;
        }

        if(_isThereNewBody)
        {
            _bodyPositions.Add(previousPosition);
            _isThereNewBody = false;
        }
    }
    void SetDirection()
    {
        if (_keyboardStateCurrent.IsKeyDown(Keys.Up) && _keyboardStatePrevious.IsKeyUp(Keys.Up) && _direction != Direction.Down)
        {
            _direction = Direction.Up;
        }
        if (_keyboardStateCurrent.IsKeyDown(Keys.Down) && _keyboardStatePrevious.IsKeyUp(Keys.Down) && _direction != Direction.Up)
        {
            _direction = Direction.Down;
        }
        if (_keyboardStateCurrent.IsKeyDown(Keys.Left) && _keyboardStatePrevious.IsKeyUp(Keys.Left) && _direction != Direction.Right)
        {
            _direction = Direction.Left;
        }
        if (_keyboardStateCurrent.IsKeyDown(Keys.Right) && _keyboardStatePrevious.IsKeyUp(Keys.Right) && _direction != Direction.Left)
        {
            _direction = Direction.Right;
        }
    }
    void CheckCollision()
    {
        Rectangle headBounds = GetHeadBounds();
        if (_headPosition.X < 0 || _headPosition.X > _viewport.Width || _headPosition.Y < 0 || _headPosition.Y > _viewport.Height || GetBodyBounds().Any(x => x.Intersects(headBounds)))
        {
            _isAlive = false;
            _color = Color.Red;            
        }
    }
    public Rectangle GetHeadBounds() => new((int)_headPosition.X - 32, (int)_headPosition.Y - 32, _textureHead.Width, _textureHead.Height);
    public List<Rectangle> GetBodyBounds() => _bodyPositions
        .ConvertAll(position => new Rectangle((int)position.X - 32, (int)position.Y - 32, _textureBody.Width, _textureBody.Height));
    public void Reset()
    {
        _bodyPositions.Clear();
        _bodyPositions.Add(new Vector2(32, 32));
        _headPosition = new(64 + 32, 32);
        _direction = Direction.Right;
        _isAlive = true;
        _rotation = 0;
        _color = Color.White;
        _currentSpeed = SPEED;
    }
}
