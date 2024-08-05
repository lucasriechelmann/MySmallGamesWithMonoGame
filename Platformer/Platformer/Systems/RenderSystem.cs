using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;

namespace Platformer.Systems;
public class RenderSystem : EntityDrawSystem
{    
    readonly SpriteBatch _spriteBatch;
    readonly OrthographicCamera _camera;
    TiledMap _map;
    TiledMapRenderer _renderer;
    ComponentMapper<AnimatedSprite> _animatedSpriteMapper;
    ComponentMapper<Sprite> _spriteMapper;
    ComponentMapper<Transform2> _transformMapper;
    public RenderSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : base(Aspect.All())
    {
        _spriteBatch = spriteBatch;
        _camera = camera;        
    }    
    public override void Initialize(IComponentMapperService mapperService)
    {
        _animatedSpriteMapper = mapperService.GetMapper<AnimatedSprite>();
        _spriteMapper = mapperService.GetMapper<Sprite>();
        _transformMapper = mapperService.GetMapper<Transform2>();
    }
    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());

        foreach (var entity in ActiveEntities)
        {
            var sprite = _animatedSpriteMapper.Has(entity)
                ? _animatedSpriteMapper.Get(entity)
                : _spriteMapper.Get(entity);
            var transform = _transformMapper.Get(entity);

            if (sprite is AnimatedSprite animatedSprite)
                animatedSprite.Update(gameTime);

            if(sprite != null && transform != null)
                _spriteBatch.Draw(sprite, transform);

        }

        _spriteBatch.End();
    }    
}
