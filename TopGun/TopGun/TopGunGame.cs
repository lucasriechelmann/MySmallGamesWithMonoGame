using AnasStudio.Engine;
using TopGun.States;

namespace TopGun;

public class TopGunGame : ExtendedGame
{
    public TopGunGame()
    {
        IsMouseVisible = true;
    }
    protected override void LoadContent()
    {
        base.LoadContent();
        GameStateManager.AddGameState(GameStateConstants.TITLE_MENU, new TitleMenuState());
        GameStateManager.AddGameState(GameStateConstants.PLAYING, new PlayingState());
        GameStateManager.SwitchTo(GameStateConstants.TITLE_MENU);
    }
}
