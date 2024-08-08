using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Graphics;
using Platformer.Components;

namespace Platformer.Systems;

public class EnemyRenderSystem : EntityDrawSystem
{
    readonly SpriteBatch _spriteBatch;
    readonly OrthographicCamera _camera;
    ComponentMapper<AnimatedSprite> _animatedSpriteMapper;
    ComponentMapper<Sprite> _spriteMapper;
    ComponentMapper<BodyComponent> _bodyMapper;
    public EnemyRenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All(typeof(EnemyComponent), typeof(BodyComponent), typeof(Sprite)))
    {
        _spriteBatch = spriteBatch;
        _camera = camera;
    }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _animatedSpriteMapper = mapperService.GetMapper<AnimatedSprite>();
        _spriteMapper = mapperService.GetMapper<Sprite>();
        _bodyMapper = mapperService.GetMapper<BodyComponent>();
    }
    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());

        foreach (var entity in ActiveEntities)
        {
            var sprite = _animatedSpriteMapper.Has(entity)
                ? _animatedSpriteMapper.Get(entity)
                : _spriteMapper.Get(entity);

            BodyComponent body = _bodyMapper.Get(entity);

            if (sprite is AnimatedSprite animatedSprite)
                animatedSprite.Update(gameTime);

            if (sprite is not null && body is not null && body.Transform is not null)
                _spriteBatch.Draw(sprite, body.Transform);

        }

        _spriteBatch.End();
    }

    
}
