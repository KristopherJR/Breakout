using Breakout.Game_Code.Entities;
using Breakout.Game_Code.Interfaces;
using Breakout.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Breakout.Game_Code
{
    public class BreakoutGame : Game
    {
        #region FIELDS
        public const int WINDOW_WIDTH = 1200;
        public const int WINDOW_HEIGHT = 800;

        public const int BRICK_ROWS = 6;
        public const int BRICK_COLUMNS = 10;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<IGameEntity> _gameEntities;

        private Ball _ball;
        private Paddle _paddle;
        #endregion

        public BreakoutGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content/breakout_assets";
            IsMouseVisible = true;

            _gameEntities = new List<IGameEntity>(); 
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameContent.LoadContent(Content);

            _paddle = new Paddle();
            _ball = new Ball();
            _gameEntities.Add(_paddle);
            _gameEntities.Add(_ball);

            for (int i = 0; i < BRICK_COLUMNS; i++)
            {
                for (int j = 0; j < BRICK_ROWS; j++)
                {
                    IGameEntity newBrick = new Brick();
                    newBrick.Location = new Vector2(15+((GameContent.RedBrickTexture.Width+10)*i),
                                                    86+(GameContent.RedBrickTexture.Height+10)*j);
                    _gameEntities.Add(newBrick);
                }
            }

            _ball.PopulateCollidables(_gameEntities);

            this.SetIDs();

            foreach(IGameEntity g in _gameEntities)
            {
                if(g is Brick)
                {   
                    (g as Brick).SetTexture();
                }
            }
        }

        private void SetIDs()
        {
            int ballCount = 1;
            int paddleCount = 1;
            int brickCount = 1;

            foreach (IGameEntity g in _gameEntities)
            {
                if (g is Ball)
                {
                    g.UID = ballCount;
                    g.UName += ballCount;
                    ballCount++;
                }
                if (g is Paddle)
                {
                    g.UID = paddleCount;
                    g.UName += paddleCount;
                    paddleCount++;
                }

                if (g is Brick)
                {
                    g.UID = brickCount;
                    g.UName += brickCount;
                    System.Diagnostics.Debug.WriteLine(g.UName);
                    brickCount++;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IGameEntity u in _gameEntities)
            {
                if(u is IUpdatable)
                {
                    (u as IUpdatable).Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(GameContent.BackgroundTexture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);

            foreach (IGameEntity g in _gameEntities)
            {
                g.Draw(_spriteBatch);
            }

            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
