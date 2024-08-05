using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using Platformer.Components;

namespace Platformer.Systems;

public class PlayerSystem : EntityProcessingSystem
{
    OrthographicCamera _camera;
    ComponentMapper<PlayerComponent> _playerMapper;
    ComponentMapper<Sprite> _spriteMapper;
    ComponentMapper<Transform2> _transformMapper;
    ComponentMapper<BodyComponent> _bodyMapper;
    float _speed = 200;
    public PlayerSystem(OrthographicCamera camera) : 
        base(Aspect.All(typeof(BodyComponent), typeof(PlayerComponent), typeof(Sprite), typeof(Transform2)))
    {
        _camera = camera;
    }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _playerMapper = mapperService.GetMapper<PlayerComponent>();
        _spriteMapper = mapperService.GetMapper<Sprite>();
        _transformMapper = mapperService.GetMapper<Transform2>();
        _bodyMapper = mapperService.GetMapper<BodyComponent>();
    }
    public override void Process(GameTime gameTime, int entityId)
    {
        PlayerComponent player = _playerMapper.Get(entityId);
        Transform2 transform = _transformMapper.Get(entityId);
        BodyComponent body = _bodyMapper.Get(entityId);
        //Sprite sprite = _spriteMapper.Get(entityId);

        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();

        Vector2 velocity = Vector2.Zero;

        if(keyboardState.IsKeyDown(Keys.Up))
            velocity.Y -= _speed;

        if(keyboardState.IsKeyDown(Keys.Down))
            velocity.Y += _speed;

        if(keyboardState.IsKeyDown(Keys.Left))
            velocity.X -= _speed;

        if(keyboardState.IsKeyDown(Keys.Right))
            velocity.X += _speed;

        body.Velocity = velocity;
    }
}
