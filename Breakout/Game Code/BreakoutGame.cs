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

            //for(int i=0; i<BRICK_COLUMNS; i++)
            //{
            //    for(int j=0; j<BRICK_ROWS; j++)
            //    {

            //    }
            //}

            _ball.PopulateCollidables(_gameEntities);

            SetIDs();
        }

        private void SetIDs()
        {
            foreach (Ball b in _gameEntities)
            {
                int temp = 1;
                b.UID = temp;
                b.UName += temp;
                temp++;
            }
            foreach (Paddle p in _gameEntities)
            {
                int temp = 1;
                p.UID = temp;
                p.UName += temp;
                temp++;
            }
            foreach (Brick b in _gameEntities)
            {
                int temp = 1;
                b.UID = temp;
                b.UName += temp;
                temp++;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IUpdatable u in _gameEntities)
            {
                u.Update(gameTime);
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
