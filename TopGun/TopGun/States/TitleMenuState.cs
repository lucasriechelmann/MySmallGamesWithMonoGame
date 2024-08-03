using AnasStudio.Engine;
using AnasStudio.Engine.Constants;
using AnasStudio.Engine.Managers;
using AnasStudio.Engine.Objects;
using AnasStudio.Engine.States;
using AnasStudio.Engine.UI;
using Microsoft.Xna.Framework;

namespace TopGun.States;

public class TitleMenuState : GameState
{
    Button _playButton;
    SpriteGameObject _background;
    public TitleMenuState()
    {
        _background = new SpriteGameObject("Sprites/title_menu", DephtConstants.BACKGROUND);
        _background.SetSpriteDestination(new(0, 0, ExtendedGame.WorldSize.X, ExtendedGame.WorldSize.Y));
        AddChild(_background);
        _playButton = new Button("UI/spr_button_play", DephtConstants.UI);
        _playButton.OnClicked += (object sender) =>
        {
            ExtendedGame.GameStateManager.SwitchTo(GameStateConstants.PLAYING);
        };
        float x = (ExtendedGame.WorldSize.X / 2) - (_playButton.Width / 2);
        float y = (ExtendedGame.WorldSize.Y / 2) - (_playButton.Height / 2);
        _playButton.LocalPosition = new Vector2(x, y);
        AddChild(_playButton);
    }
}
