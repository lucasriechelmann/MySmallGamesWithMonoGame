using MarioClone.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;

namespace MarioClone.Systems;
public class MovementSystem : EntityUpdateSystem
{
    private ComponentMapper<TransformComponent> _transformMapper;
    private ComponentMapper<VelocityComponent> _velocityMapper;

    public MovementSystem() : base(Aspect.All(typeof(TransformComponent), typeof(VelocityComponent)))
    {
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<TransformComponent>();
        _velocityMapper = mapperService.GetMapper<VelocityComponent>();
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var entityId in ActiveEntities)
        {
            var transform = _transformMapper.Get(entityId);
            var velocity = _velocityMapper.Get(entityId);

            transform.Position += velocity.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}

