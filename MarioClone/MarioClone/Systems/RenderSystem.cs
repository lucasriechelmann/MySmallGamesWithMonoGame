using MarioClone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Graphics;

namespace MarioClone.Systems;

public class RenderSystem : EntityDrawSystem
{
    private ComponentMapper<TransformComponent> _transformMapper;
    private ComponentMapper<PlayerAnimationComponent> _spriteMapper;
    private SpriteBatch _spriteBatch;
    OrthographicCamera _camera;

    public RenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All(typeof(TransformComponent), typeof(PlayerAnimationComponent)))
    {
        _spriteBatch = spriteBatch;
        _camera = camera;
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<TransformComponent>();
        _spriteMapper = mapperService.GetMapper<PlayerAnimationComponent>();
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());

        foreach (var entityId in ActiveEntities)
        {
            var transform = _transformMapper.Get(entityId);
            var sprite = _spriteMapper.Get(entityId);

            Transform2 transform2 = new Transform2(transform.Position, transform.Rotation, transform.Scale);
            _spriteBatch.Draw(sprite.Sprite, transform2);
        }

        _spriteBatch.End();
    }
}

