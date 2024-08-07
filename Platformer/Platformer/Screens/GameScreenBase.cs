using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;

namespace Platformer.Screens;

public abstract class GameScreenBase : GameScreen
{
    protected GameMain GameMain => (GameMain)Game;
    protected World _world;
    protected EntityFactory _entityFactory;
    bool _loaded;
    public GameScreenBase(GameMain game) : base(game)
    {
        
    }
    public override void Draw(GameTime gameTime)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        
    }
    public override void LoadContent()
    {
        if(_loaded)
            return;

        _loaded = true;

        Load();
    }
    protected abstract void Load();
}
