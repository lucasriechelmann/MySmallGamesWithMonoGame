using AnasStudio.Engine;
using AnasStudio.Engine.Constants;
using AnasStudio.Engine.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopGun.GameObjects
{
    public class Shoot : SpriteGameObject
    {        
        public Shoot(float speed) : base("Sprites/shoot", DephtConstants.PLAYER)
        {            
            SetOriginToCenter();
            _velocity.Y = speed;
            
            if(speed > 0)
                ChangeInvertVertical();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GlobalPosition.Y - Height < 0)
                Visible = false;

            if (GlobalPosition.Y + Height / 2 > ExtendedGame.WorldSize.Y)
                Visible = false;
        }
    }
}
