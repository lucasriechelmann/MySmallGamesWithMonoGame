using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Tiled.Renderers;

namespace MarioClone.Systems;

public class TiledMapRenderSystem : DrawSystem
{
    private readonly TiledMapRenderer _tiledMapRenderer;
    private readonly OrthographicCamera _camera;
    SpriteBatch _spriteBatch;
    public TiledMapRenderSystem(TiledMapRenderer tiledMapRenderer, OrthographicCamera camera, SpriteBatch spriteBatch)
    {
        _tiledMapRenderer = tiledMapRenderer;
        _camera = camera;
        _spriteBatch = spriteBatch;
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.GetViewMatrix());
        _tiledMapRenderer.Draw(0, _camera.GetViewMatrix());
        _spriteBatch.End();
    }
}
