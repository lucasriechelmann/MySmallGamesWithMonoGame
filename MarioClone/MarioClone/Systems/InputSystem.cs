using MarioClone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Input;

namespace MarioClone.Systems;

public class InputSystem : EntityProcessingSystem
{
    const string WALK = "Walk";
    const string JUMP = "Jump";
    const string IDLE = "Idle";
    string _animation = IDLE;
    bool _inverted = false;
    ComponentMapper<InputComponent> _inputMapper;
    ComponentMapper<VelocityComponent> _velocityMapper;
    ComponentMapper<PlayerAnimationComponent> _playerAnimationMapper;
    public InputSystem() : base(Aspect.All(typeof(InputComponent), typeof(VelocityComponent), typeof(PlayerAnimationComponent)))
    {
        
    }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _velocityMapper = mapperService.GetMapper<VelocityComponent>();
        _inputMapper = mapperService.GetMapper<InputComponent>();
        _playerAnimationMapper = mapperService.GetMapper<PlayerAnimationComponent>();
    }

    public override void Process(GameTime gameTime, int entityId)
    {
        KeyboardExtended.Update();
        var keyboardState = KeyboardExtended.GetState();

        var direction = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
        {
            direction.Y -= 1;
            _animation = JUMP;
        }

        if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
        {
            direction.Y += 1;

        }

        if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
        {
            direction.X -= 1;
            _animation = WALK;
            _inverted = true;
        }
        if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
        {
            direction.X += 1;
            _animation = WALK;
            _inverted = false;
        }

        if (!keyboardState.IsKeyDown(Keys.A) && !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.D) && !keyboardState.IsKeyDown(Keys.Right))
        {
            _animation = IDLE;
        }

        var input = _inputMapper.Get(entityId);
        var velocity = _velocityMapper.Get(entityId);
        var playerAnimation = _playerAnimationMapper.Get(entityId);

        playerAnimation.SetInverted(_inverted);

        input.Direction = direction;
        velocity.Velocity = input.Direction * 150f; // Adjust speed as needed

        playerAnimation.Play(_animation);
        playerAnimation.Update(gameTime);
    }

}
