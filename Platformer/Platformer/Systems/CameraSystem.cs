using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Platformer.Components;

namespace Platformer.Systems;

public class CameraSystem : EntityProcessingSystem
{
    OrthographicCamera _camera;
    ComponentMapper<BodyComponent> _bodyMapper;
    GameMain _game;
    public CameraSystem(GameMain game, OrthographicCamera camera) : base(Aspect.All(typeof(PlayerComponent), typeof(BodyComponent)))
    {
        _camera = camera;
        _game = game;
    }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _bodyMapper = mapperService.GetMapper<BodyComponent>();
    }

    public override void Process(GameTime gameTime, int entityId)
    {
        BodyComponent body = _bodyMapper.Get(entityId);

        // Calculate half of the viewport dimensions
        float halfViewportWidth = _camera.BoundingRectangle.Width / 2f;
        float halfViewportHeight = _camera.BoundingRectangle.Height / 2f;

        // Calculate the clamped camera position
        float clampedX = MathHelper.Clamp(body.Position.X, halfViewportWidth, _game.WorldSize.Width - halfViewportWidth);
        float clampedY = MathHelper.Clamp(body.Position.Y, halfViewportHeight, _game.WorldSize.Height - halfViewportHeight);

        // Set the camera position
        _camera.LookAt(new Vector2(clampedX, clampedY));
    }
}
