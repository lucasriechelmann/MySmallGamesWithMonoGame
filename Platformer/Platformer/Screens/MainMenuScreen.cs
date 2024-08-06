using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using static Platformer.GameMain;

namespace Platformer.Screens;

public class MainMenuScreen : BaseGameScreen
{
    public MainMenuScreen(GameMain game) : base(game)
    {
        
    }
    public override void LoadContent()
    {
        
    }
    public override void Draw(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {
        if(KeyboardExtended.GetState().IsKeyDown(Keys.F2))
            GameMain.LoadScreen(GameState.Gameplay);
    }
}
