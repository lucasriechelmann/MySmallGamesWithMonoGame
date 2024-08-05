using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer;

public abstract class GameBase : Game
{
    protected GraphicsDeviceManager Graphics { get; }
    protected IContainer Container { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Viewport Viewport => Graphics.GraphicsDevice.Viewport;
    protected GameBase(int width = 800, int height = 480)
    {
        Width = width;
        Height = height;
        Graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Width,
            PreferredBackBufferHeight = Height
        };
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Content.RootDirectory = "Content";
    }
    protected override void Initialize()
    {
        var builder = new ContainerBuilder();
        RegisterDependencies(builder);
        Container = builder.Build();

        base.Initialize();
    }
    protected abstract void RegisterDependencies(ContainerBuilder builder);
}