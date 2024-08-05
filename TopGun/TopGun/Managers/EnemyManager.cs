using AnasStudio.Engine;
using AnasStudio.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGun.GameObjects;

namespace TopGun.Managers;

public class EnemyManager
{
    GameObjectList _helicopters = new();
    GameObjectList _shoots = new();
    public GameObjectList Helicopters => _helicopters;
    public GameObjectList Shoots => _shoots;
    float _helicopterSpawnElapsedTime = 0;
    float _helicopterSpawnCooldown = 3000;
    public void Update(GameTime gameTime)
    {
        CreateHelicopter(gameTime);
        _helicopters.Update(gameTime);
        _shoots.Update(gameTime);

        _helicopters.RemoveNotVisible();
        _shoots.RemoveNotVisible();
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _helicopters.Draw(gameTime, spriteBatch);
        _shoots.Draw(gameTime, spriteBatch);
    }
    void CreateHelicopter(GameTime gameTime)
    {
        _helicopterSpawnElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if(_helicopterSpawnElapsedTime > _helicopterSpawnCooldown)
        {
            Helicopter helicopter = new();
            helicopter.LocalPosition = new Vector2(ExtendedGame.Random.Next(0, ExtendedGame.WorldSize.X - 64), -64);
            helicopter.OnShoot += Shoot;
            _helicopters.AddChild(helicopter);
            _helicopterSpawnElapsedTime = 0;
            _helicopterSpawnCooldown = ExtendedGame.Random.Next(2000, 5000);
        }            
    }
    void Shoot(Vector2 position)
    {
        Shoot shoot = new(200);
        shoot.LocalPosition = new Vector2(position.X, position.Y + 25);
        _shoots.AddChild(shoot);
    }
}
