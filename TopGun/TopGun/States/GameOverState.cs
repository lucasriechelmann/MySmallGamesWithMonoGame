using AnasStudio.Engine;
using AnasStudio.Engine.Constants;
using AnasStudio.Engine.Managers;
using AnasStudio.Engine.Objects;
using AnasStudio.Engine.States;
using Microsoft.Xna.Framework;

namespace TopGun.States;

public class  GameOverState : GameState
{    
    public GameOverState()
    {
        SpriteGameObject gameOver = new("UI/spr_gameover", DephtConstants.BACKGROUND);
        Vector2 position = new(ExtendedGame.WorldSize.X / 2 - gameOver.Width / 2, ExtendedGame.WorldSize.Y / 2 - gameOver.Height / 2);
        gameOver.LocalPosition = position;
        AddChild(gameOver);
    }
    public override void HandleInput(InputManager inputManager)
    {
        base.HandleInput(inputManager);

        if (inputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
        {
            ExtendedGame.GameStateManager.SwitchTo(GameStateConstants.PLAYING);
            ExtendedGame.GameStateManager.Reset();
        }
    }
}   
