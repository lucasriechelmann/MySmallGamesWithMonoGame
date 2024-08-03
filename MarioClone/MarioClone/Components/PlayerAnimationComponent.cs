using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;

namespace MarioClone.Components;

public class PlayerAnimationComponent
{
    public AnimatedSprite Sprite => _sprite;
    AnimatedSprite _sprite;
    public PlayerAnimationComponent(Texture2D texture)
    {
        Texture2DAtlas marioAtlas = Texture2DAtlas.Create("TextureAtlas//mario", texture, 18, 18);
        SpriteSheet spriteSheet = new SpriteSheet("TextureAtlas//mario", marioAtlas);
        
        spriteSheet.DefineAnimation("Idle", builder =>
        {
            builder.IsLooping(false);
            builder.AddFrame(6, TimeSpan.FromSeconds(0.5));
        });
        spriteSheet.DefineAnimation("Walk", builder =>
        {
            builder.IsLooping(true);
            builder.AddFrame(0, TimeSpan.FromSeconds(0.125));
            builder.AddFrame(1, TimeSpan.FromSeconds(0.125));
            builder.AddFrame(2, TimeSpan.FromSeconds(0.125));
            builder.AddFrame(3, TimeSpan.FromSeconds(0.125));
        });
        spriteSheet.DefineAnimation("Jump", builder =>
        {
            builder.IsLooping(false);
            builder.AddFrame(4, TimeSpan.FromSeconds(0.150));
            builder.AddFrame(5, TimeSpan.FromSeconds(0.150));
        });
        _sprite = new AnimatedSprite(spriteSheet, "Idle");
    }
    public void Play(string animation)
    {
        if (_sprite.CurrentAnimation == animation)
            return;        

        IAnimationController controller = _sprite.SetAnimation(animation);        
    }
    public void Stop() => _sprite.Controller.Stop();
    public void Update(GameTime gameTime) => _sprite.Update(gameTime);
    public void SetInverted(bool inverted) => _sprite.Effect = inverted ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
}
