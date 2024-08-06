using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Platformer.Components;
using Platformer.Screens;
using Platformer.Systems;
using System.Collections.Generic;

namespace Platformer;

public class GameMain : GameBase
{
    public enum GameState
    {
        MainMenu,
        Gameplay
    }
    Dictionary<GameState, GameScreen> _screens = new();
    OrthographicCamera _camera;
    public Size WorldSize { get; set; }
    ScreenManager _screenManager;
    public GameMain() : base(1280, 720)
    {
        
    }
    protected override void RegisterDependencies(ContainerBuilder builder)
    {
        _camera = new OrthographicCamera(GraphicsDevice);
        builder.RegisterInstance(this);
        builder.RegisterInstance(new SpriteBatch(GraphicsDevice));
        builder.RegisterInstance(_camera);
        builder.RegisterInstance(GraphicsDevice);
        builder.RegisterInstance(Content);
        builder.RegisterType<PlayerSystem>();
        builder.RegisterType<RenderSystem>();
        builder.RegisterType<CollisionSystem>();
        builder.RegisterType<DebugTileSystem>();
        builder.RegisterType<CameraSystem>();
        builder.RegisterType<GameplayScreen>();
        builder.RegisterType<MainMenuScreen>();
    }
    protected override void LoadContent()
    {
        _screenManager = new();
        Components.Add(_screenManager);  
        LoadScreen(GameState.MainMenu);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardExtended.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Draw(gameTime);
    }
    public void LoadScreen(GameState state)
    {
        _screenManager.LoadScreen(GetScreen(state), new FadeTransition(GraphicsDevice, Color.Black));
    }
    GameScreen GetScreen(GameState state)
    {
        GameScreen gameScreen = _screens.GetValueOrDefault(state);

        if (gameScreen is null)
        {
            gameScreen = state switch
            {
                GameState.MainMenu => Container.Resolve<MainMenuScreen>(),
                GameState.Gameplay => Container.Resolve<GameplayScreen>(),
                _ => null
            };

            _screens.Add(state, gameScreen);
        }

        return gameScreen;
    }
}
