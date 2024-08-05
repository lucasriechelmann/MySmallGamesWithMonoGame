using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Platformer.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Systems
{
    public class DebugTileSystem : EntityDrawSystem
    {
        OrthographicCamera _camera;
        SpriteBatch _spriteBatch;
        Texture2D _debugTexture;
        Rectangle _source;
        ComponentMapper<BodyComponent> _bodyMapper;
        public DebugTileSystem(SpriteBatch spriteBatch, ContentManager contentManager, OrthographicCamera camera) : base(Aspect.All(typeof(BodyComponent)))
        {
            _spriteBatch = spriteBatch;
            _debugTexture = contentManager.Load<Texture2D>("Tiled/Sprites/collisionbox16x16");
            _source = new Rectangle(0, 0, 16, 16);
            _camera = camera;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _bodyMapper = mapperService.GetMapper<BodyComponent>();
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());
            foreach(int entityId in ActiveEntities)
            {
                BodyComponent body = _bodyMapper.Get(entityId);
                var position = body.Position;
                var size = body.Size;
                Rectangle destination = new((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
                
                _spriteBatch.Draw(_debugTexture, destination, _source, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            }
            _spriteBatch.End();
        }        
    }
}
