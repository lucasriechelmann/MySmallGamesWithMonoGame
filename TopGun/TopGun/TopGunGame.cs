using AnasStudio.Engine;
using Microsoft.Xna.Framework.Graphics;
using TopGun.States;

namespace TopGun;

public class TopGunGame : ExtendedGame
{
    public TopGunGame()
    {
        IsMouseVisible = true;
        _samplerState = SamplerState.PointClamp;
    }
    protected override void LoadContent()
    {
        base.LoadContent();
        GameStateManager.AddGameState(GameStateConstants.TITLE_MENU, new TitleMenuState());
        GameStateManager.AddGameState(GameStateConstants.PLAYING, new PlayingState());
        GameStateManager.AddGameState(GameStateConstants.GAME_OVER, new GameOverState());
        GameStateManager.SwitchTo(GameStateConstants.TITLE_MENU);
    }
}
