using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;

namespace Platformer.Screens;

public class BaseGameScreen : GameScreen
{
    protected GameMain GameMain => (GameMain)Game;
    protected World _world;
    protected EntityFactory _entityFactory;
    public BaseGameScreen(GameMain game) : base(game)
    {
        
    }
    public override void Draw(GameTime gameTime)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        
    }
}
