using MonoGame.Extended.ECS;
using MonoGame.Extended;
using MonoGame.Extended.ECS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Components;
using Microsoft.Xna.Framework;

namespace MarioClone.Systems
{
    
    public class CameraFollowSystem : EntityProcessingSystem
    {
        OrthographicCamera _camera;
        ComponentMapper<TransformComponent> _transformMapper;
        public CameraFollowSystem(OrthographicCamera camera) : base(Aspect.All(typeof(TransformComponent), typeof(InputComponent)))
        {
            _camera = camera;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<TransformComponent>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transform = _transformMapper.Get(entityId);
            var cameraPosition = new Vector2((int)(transform.Position.X + 0.5), (int)(transform.Position.Y + 0.5));
            _camera.LookAt(cameraPosition);
        }
    }
}
