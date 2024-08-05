using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Platformer.Components;

namespace Platformer.Systems;

public class CameraSystem : EntityProcessingSystem
{
    OrthographicCamera _camera;
    ComponentMapper<Transform2> _transformMapper;
    GameMain _game;
    public CameraSystem(GameMain game, OrthographicCamera camera) : base(Aspect.All(typeof(PlayerComponent), typeof(Transform2)))
    {
        _camera = camera;
        _game = game;
    }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
    }

    public override void Process(GameTime gameTime, int entityId)
    {
        Transform2 transform = _transformMapper.Get(entityId);

        // Calculate half of the viewport dimensions
        float halfViewportWidth = _camera.BoundingRectangle.Width / 2f;
        float halfViewportHeight = _camera.BoundingRectangle.Height / 2f;

        // Calculate the clamped camera position
        float clampedX = MathHelper.Clamp(transform.Position.X, halfViewportWidth, _game.WorldSize.Width - halfViewportWidth);
        float clampedY = MathHelper.Clamp(transform.Position.Y, halfViewportHeight, _game.WorldSize.Height - halfViewportHeight);

        // Set the camera position
        _camera.LookAt(new Vector2(clampedX, clampedY));
    }
}
