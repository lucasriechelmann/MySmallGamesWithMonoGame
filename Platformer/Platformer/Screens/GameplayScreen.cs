using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Input;
using Platformer.Enums;
using Platformer.Managers;
using Platformer.Systems;
using static Platformer.GameMain;

namespace Platformer.Screens;

public class GameplayScreen : GameScreenBase
{
    OrthographicCamera _camera;
    LevelManager _levelManager ;
    public GameplayScreen(GameMain game, OrthographicCamera camera) : base(game)
    {        
        _camera = camera;
    }
    public override void Update(GameTime gameTime)
    {
        if (KeyboardExtended.GetState().IsKeyDown(Keys.F1))
        {
            GameMain.LoadScreen(GameState.MainMenu);
            return;
        }

        _levelManager?.Update(gameTime);
        _world.Update(gameTime);
    }
    public override void Draw(GameTime gameTime)
    {
        _levelManager?.Draw(gameTime);
        _world?.Draw(gameTime);
    }
    protected override void Load()
    {
        _world = new WorldBuilder()
                .AddSystem(GameMain.Container.Resolve<PlayerSystem>())
                .AddSystem(GameMain.Container.Resolve<EnemySystem>())
                .AddSystem(GameMain.Container.Resolve<CollisionSystem>())
                .AddSystem(GameMain.Container.Resolve<CameraSystem>())
                .AddSystem(GameMain.Container.Resolve<PlayerRenderSystem>())
                .AddSystem(GameMain.Container.Resolve<EnemyRenderSystem>())
                //.AddSystem(GameMain.Container.Resolve<DebugTileSystem>())
                .Build();

        _levelManager = new LevelManager(GameMain, _camera, _world);
        _levelManager.AddLevel("level1", "Tiled/Map/level1", 0);
        _levelManager.ChangeLevel("level1");        
    }    
}