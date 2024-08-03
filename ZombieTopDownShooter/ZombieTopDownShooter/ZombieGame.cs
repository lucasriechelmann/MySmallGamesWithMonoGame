using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieTopDownShooter
{
    public class ZombieGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _tileset;
        Player _player;
        List<Zombie> _zombies = new();
        List<Shoot> _shoots = new();
        Viewport _viewport;
        float _zombieSpawnTime = 0;
        Random _random = new();
        public ZombieGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _viewport = GraphicsDevice.Viewport;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileset = Content.Load<Texture2D>("zombie_tileset");
            _player = new(_tileset, new(_viewport.Width / 2, _viewport.Height / 2), _shoots, _viewport);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            foreach (Zombie zombie in _zombies.Where(x => x.IsAlive))
            {
                zombie.Update(gameTime, _player);
            }
            foreach(Shoot shoot in _shoots.Where(x => x.IsAlive))
            {
                shoot.Update(gameTime);
            }
            _player.Update(gameTime);
            ZombieSpawner(gameTime);
            HandleCollisions();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();            
            foreach (Zombie zombie in _zombies.Where(x => x.IsAlive))
            {
                zombie.Draw(_spriteBatch);
            }
            foreach(Shoot shoot in _shoots.Where(x => x.IsAlive))
            {
                shoot.Draw(_spriteBatch);
            }
            _player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        void HandleCollisions()
        {
            foreach(Zombie zombie in _zombies.Where(x => x.IsAlive))
            {
                if (zombie.Bounds.Intersects(_player.Bounds))
                {
                    _player.Die();
                }
                foreach (Shoot shoot in _shoots.Where(x => x.IsAlive))
                {
                    if (zombie.Bounds.Intersects(shoot.Bounds))
                    {
                        zombie.Die();
                        shoot.Destroy();
                    }
                }
            }
            _zombies.RemoveAll(x => !x.IsAlive);
            _shoots.RemoveAll(x => !x.IsAlive);
        }
        void ZombieSpawner(GameTime gameTime)
        {
            _zombieSpawnTime += (float)gameTime.ElapsedGameTime.Milliseconds;
            if(_zombieSpawnTime > 350)
            {
                _zombieSpawnTime = 0;

                Zombie zombie = _zombies.Find(x => !x.IsAlive);

                Vector2 position = GetRandomPositionOutsideScreen();

                if(zombie is null)
                {
                    zombie = new(_tileset, position, _viewport, _graphics.GraphicsDevice);
                    _zombies.Add(zombie);
                    return;
                }

                zombie.Respawn(position);
            }
        }
        Vector2 GetRandomPositionOutsideScreen() 
        {             
            int side = _random.Next(0, 4);
            int x = 0;
            int y = 0;
            switch (side)
            {
                case 0:
                    x = _random.Next(0, _viewport.Width);
                    y = -64;
                    break;
                case 1:
                    x = _random.Next(0, _viewport.Width);
                    y = _viewport.Height + 64;
                    break;
                case 2:
                    x = -64;
                    y = _random.Next(0, _viewport.Height);
                    break;
                case 3:
                    x = _viewport.Width + 64;
                    y = _random.Next(0, _viewport.Height);
                    break;
            }
            return new(x, y);
        }
    }
}
