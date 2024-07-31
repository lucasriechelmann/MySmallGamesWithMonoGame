using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlapSeagull
{
    public class FlapSeagullGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Background _background;
        Seagull _seagull;
        List<Cloud> _clouds;
        List<Pipes> _pipes;
        Viewport _viewport => _graphics.GraphicsDevice.Viewport;
        int _pipe1PositionX;
        int _pipe2PositionX;
        public FlapSeagullGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = new(Content.Load<Texture2D>("background"), _viewport.Width, _viewport.Height);
            Texture2D seagullTexture = Content.Load<Texture2D>("seagull2");
            _seagull = new(seagullTexture, new Vector2(100, _viewport.Height / 2 - seagullTexture.Height / 2), 32, 32, 8, 50, _viewport);
            Texture2D cloudTexture = Content.Load<Texture2D>("cloud");
            _clouds = new List<Cloud>()
            {
                new(_viewport, cloudTexture, new Vector2(30, 30)),
                new(_viewport, cloudTexture, new Vector2(_viewport.Width / 2, _viewport.Height / 2)),
                new(_viewport, cloudTexture, new Vector2(_viewport.Width - 150, _viewport.Height - 150)),
                new(_viewport, cloudTexture, new Vector2(_viewport.Width  / 3, _viewport.Height / 3)),
            };
            Texture2D pipeTexture = Content.Load<Texture2D>("pipe");
            _pipe1PositionX = _viewport.Width;
            _pipe2PositionX = (int)Math.Round(_viewport.Width * 1.5F) + 100;
            _pipes = new List<Pipes>()
            {
                new(pipeTexture, _viewport.Width, _viewport.Height, _pipe1PositionX),
                new(pipeTexture, _viewport.Width, _viewport.Height, _pipe2PositionX)
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_seagull.IsAlive)
            {
                foreach (Cloud cloud in _clouds)
                {
                    cloud.Update(gameTime);
                }
                foreach (Pipes pipe in _pipes)
                {
                    pipe.Update(gameTime);
                }
                _seagull.Update(gameTime);

                CheckCollision();
            }

            if (!_seagull.IsAlive)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Enter))
                {
                    _seagull.Reset();
                    _pipes[0].Reset(_pipe1PositionX);
                    _pipes[1].Reset(_pipe2PositionX);
                }
            }

            base.Update(gameTime);
        }
        void CheckCollision()
        {
            Rectangle seagullBounds = _seagull.GetBounds();
            foreach (Pipes pipe in _pipes)
            {
                Rectangle pipeTopBounds = pipe.GetTopBounds();
                Rectangle pipeBottomBounds = pipe.GetBottomBounds();
                if (seagullBounds.Intersects(pipeTopBounds) || seagullBounds.Intersects(pipeBottomBounds))
                {
                    _seagull.Die();
                    break;
                }
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _background.Draw(_spriteBatch);
            foreach (Cloud cloud in _clouds)
            {
                cloud.Draw(gameTime, _spriteBatch);
            }
            foreach (Pipes pipe in _pipes)
            {
                pipe.Draw(gameTime, _spriteBatch);
            }
            _seagull.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
