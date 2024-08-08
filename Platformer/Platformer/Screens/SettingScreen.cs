using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Platformer.Enums;
using static Platformer.GameMain;

namespace Platformer.Screens;
public class SettingScreen : GameScreenBase
{
    public SettingScreen(GameMain game) : base(game)
    {
        
    }
    public override void Update(GameTime gameTime)
    {
        if (KeyboardExtended.GetState().IsKeyDown(Keys.F1))
            GameMain.LoadScreen(GameState.MainMenu);
    }
    public override void Draw(GameTime gameTime)
    {
        
    }
    protected override void Load()
    {
        
    }
}
