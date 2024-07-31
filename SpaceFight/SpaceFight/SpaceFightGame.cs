using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceFight
{
    public class SpaceFightGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _playerTexture;
        Texture2D _enemyTexture;
        Texture2D _shootTexture;
        Texture2D _starTexture;
        Player _player;
        List<Enemy> _enemies;
        List<Shoot> _shoots;
        List<Star> _stars;
        float _enemySpawnTime = 0;
        float _enemySpawnRate = 2000;
        Viewport _viewport => _graphics.GraphicsDevice.Viewport;
        Random _random = new();
        public SpaceFightGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerTexture = Content.Load<Texture2D>("spaceship");
            _enemyTexture = Content.Load<Texture2D>("enemy");
            _shootTexture = Content.Load<Texture2D>("shoot");
            

            float midleX = _viewport.Width / 2;

            _player = new(_playerTexture, new Vector2(midleX - (_playerTexture.Width / 2), _viewport.Height - _playerTexture.Height - 10), _viewport);
            _player.Shoot += Shoot;
            _enemies = new();
            _shoots = new();
            CreateStars();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CreateEnemy(gameTime);

            _player.Update(gameTime);
            
            foreach(Enemy enemy in _enemies)
                enemy.Update(gameTime);

            foreach(Shoot shoot in _shoots)
                shoot.Update(gameTime);

            foreach(Star star in _stars)
                star.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch);

            foreach(Enemy enemy in _enemies)
                enemy.Draw(_spriteBatch);

            foreach(Shoot shoot in _shoots)
                shoot.Draw(_spriteBatch);

            foreach(Star star in _stars)
                star.Draw(_spriteBatch);

            ValidateEnemyKill();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void CreateEnemy(GameTime gameTime)
        {
            _enemySpawnTime += (float)gameTime.ElapsedGameTime.Milliseconds;

            if(_enemySpawnTime < _enemySpawnRate)
                return;

            _enemySpawnTime = 0;
            _enemySpawnRate = _random.Next(500, 2500);

            Vector2 position = new(_random.Next(10, _viewport.Width - 10 - _enemyTexture.Width), 0 - _enemyTexture.Height);
            float yVelocity = _random.Next(50, 200) / 100;

            Enemy enemy = _enemies.Where(x => x.IsRemoved).FirstOrDefault();

            if (enemy is null)
            {
                enemy = new(_enemyTexture, position, yVelocity, _viewport);
                _enemies.Add(enemy);
                return;
            }
            
            enemy.Reset();
            enemy.SetPosition(position);
        }
        void CreateStars()
        {
            _starTexture = Content.Load<Texture2D>("star");
            _stars = new();
            for (int w = 0; w < _viewport.Width; w++)
            {
                for (int h = 0; h < _viewport.Height; h++)
                {
                    if (w % 10 == 0 && h % 10 == 0 && _random.Next(0, 100) > 97)
                        _stars.Add(new(_starTexture, new Vector2(w, h), _viewport));
                }
            }
        }
        void Shoot(Vector2 position)
        {
            Shoot shoot = _shoots.Where(x => x.IsRemoved).FirstOrDefault();

            if (shoot is null)
            {
                shoot = new(_shootTexture, position, _viewport);
                _shoots.Add(shoot);
                return;
            }

            shoot.Reset();
            shoot.SetPosition(position);
        }
        void ValidateEnemyKill()
        {
            foreach(Shoot shoot in _shoots)
            {
                if (shoot.IsRemoved)
                    continue;

                foreach(Enemy enemy in _enemies)
                {
                    if (enemy.IsRemoved)
                        continue;

                    if (shoot.GetBounds().Intersects(enemy.GetBounds()))
                    {
                        shoot.Remove();
                        enemy.Remove();
                    }
                }
            }
        }
    }
}
