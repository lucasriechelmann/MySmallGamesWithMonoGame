using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using Platformer.Enums;
using RenderingLibrary;
using static Platformer.GameMain;

namespace Platformer.Screens;

public class MainMenuScreen : GameScreenBase
{
    ContainerRuntime Root;
    public MainMenuScreen(GameMain game) : base(game)
    {
        
    }
    public override void Update(GameTime gameTime)
    {
        FormsUtilities.Update(gameTime, Root);
        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
    }
    public override void Draw(GameTime gameTime)
    {
        SystemManagers.Default.Draw();
    }    
    protected override void Load()
    {
        SystemManagers.Default = new SystemManagers();
        SystemManagers.Default.Initialize(GraphicsDevice, fullInstantiation: true);
        FormsUtilities.InitializeDefaults();

        Root = new();
        Root.Width = 0;
        Root.Height = 0;
        Root.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
        Root.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToContainer;
        Root.AddToManagers();
        Root.ChildrenLayout = Gum.Managers.ChildrenLayout.TopToBottomStack;

        Button btnStart = new()
        {
            Text = "Start",
            Width = 200
        };
        
        btnStart.Click += (sender, args) => GameMain.LoadScreen(GameState.Gameplay);

        Button btnSetting = new()
        {
            Text = "Settings",
            Width = 200
        };
        btnSetting.Click += (sender, args) => GameMain.LoadScreen(GameState.Setting);

        Root.Children.Add(btnStart.Visual);
        Root.Children.Add(btnSetting.Visual);
    }
}
