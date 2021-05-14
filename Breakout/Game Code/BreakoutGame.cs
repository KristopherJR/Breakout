using Breakout.Game_Code.Entities;
using Breakout.Game_Code.Interfaces;
using Breakout.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Breakout.Game_Code
{
    public class BreakoutGame : Game
    {
        #region FIELDS

        private bool _running;
        private bool _scriptStop;
        private bool _fontRed;

        private float _idleTimer;
        private float _speechWaitDuration;
        private float _flashTimer;
        private float _flashInterval;

        public const int WINDOW_WIDTH = 1200;
        public const int WINDOW_HEIGHT = 800;

        public const int BRICK_ROWS = 6;
        public const int BRICK_COLUMNS = 10;

        private int _lives;
        private int _score;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<IGameEntity> _gameEntities;

        private Ball _ball;
        private Paddle _paddle;
        private Text _scoreText;
        private Text _gameOverText;

        #endregion FIELDS

        public BreakoutGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content/breakout_assets";
            IsMouseVisible = true;

            _gameEntities = new List<IGameEntity>();
            _lives = 5;
            _score = 0;
            _running = true;
            _idleTimer = 0.0f;
            _speechWaitDuration = 4.0f;
            _flashTimer = 0.0f;
            _flashInterval = 1.0f;
            _scriptStop = false;
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

            _gameOverText = new Text(new Vector2(2000, 2000), "GAME OVER");
            _gameOverText.Font = GameContent.GameFont48;

            _scoreText = new Text(new Vector2(119, 24), _score.ToString());

            _gameEntities.Add(_paddle);
            _gameEntities.Add(_ball);

            for (int i = 0; i < BRICK_COLUMNS; i++)
            {
                for (int j = 0; j < BRICK_ROWS; j++)
                {
                    IGameEntity newBrick = new Brick();
                    newBrick.Location = new Vector2(15 + ((GameContent.RedBrickTexture.Width + 10) * i),
                                                    86 + (GameContent.RedBrickTexture.Height + 10) * j);
                    _gameEntities.Add(newBrick);
                }
            }

            _ball.PopulateCollidables(_gameEntities);

            for (int i = 0; i < _lives; i++)
            {
                IGameEntity newHeart = new Heart();
                newHeart.Location = new Vector2(995 + ((GameContent.HeartTexture.Width + 5) * i), 24);
                _gameEntities.Add(newHeart);
            }

            this.SetIDs();

            foreach (IGameEntity g in _gameEntities)
            {
                if (g is Brick)
                {
                    (g as Brick).SetTexture();
                }
            }
        }

        /// <summary>
        /// Gives each GameEntity a Unique ID number and a Unique Name.
        /// </summary>
        private void SetIDs()
        {
            int ballCount = 1;
            int paddleCount = 1;
            int brickCount = 1;
            int heartCount = 1;

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
                    brickCount++;
                }
                if (g is Heart)
                {
                    g.UID = heartCount;
                    g.UName += heartCount;
                    heartCount++;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_running)
            {
                if (!_gameEntities.OfType<Brick>().Any()) // if all bricks have been broken, player wins
                {
                    _gameOverText.Message = "YOU WIN!";
                    _gameOverText.CentreOnScreen();
                    GameContent.Victory.Play();
                    _running = false;
                }

                for (int i = 0; i < _gameEntities.Count; i++)
                {
                    if (_gameEntities[i] is IUpdatable)
                    {
                        (_gameEntities[i] as IUpdatable).Update(gameTime);
                    }
                    if (_gameEntities[i] is Brick)
                    {
                        if ((_gameEntities[i] as Brick).FlagDeletion == true) // delete brick
                        {
                            _score += (_gameEntities[i] as Brick).ScoreValue;
                            _gameEntities.Remove(_gameEntities[i]);
                        }
                    }
                    if (_gameEntities[i] is Ball)
                    {
                        if ((_gameEntities[i] as Ball).FlagLifeLost) // check if the player lost a life
                        {
                            (_gameEntities[i] as Ball).FlagLifeLost = false; // reset flag

                            for (int j = 0; j < _gameEntities.Count; j++)
                            {
                                if (_gameEntities[j] is Heart)
                                {
                                    if (_gameEntities[j].UName == "Heart" + _lives) // remove the top layer heart
                                    {
                                        _gameEntities.RemoveAt(j);
                                        _lives--;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                base.Update(gameTime);

                if (_lives == 0)
                {
                    _running = false;
                    _gameOverText.CentreOnScreen();
                    GameContent.GameOverTrumpets.Play();
                }
            }

            if (!_running && !_scriptStop)
            {
                if (_idleTimer < _speechWaitDuration)
                {
                    _idleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (_idleTimer >= _speechWaitDuration)
                {
                    _scriptStop = true;
                    GameContent.GameOverVoice.Play();
                }
            }

            if (!_running)
            {
                if (_flashTimer < _flashInterval) // wait for the total duration
                {
                    _flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (_flashTimer >= _flashInterval)
                {
                    if (_fontRed == true) // change text to red if true
                    {
                        _gameOverText.FontColor = Color.Crimson;
                        _fontRed = false;
                    }
                    else // else change it to white
                    {
                        _gameOverText.FontColor = Color.White;
                        _fontRed = true;
                    }
                    _flashTimer = 0.0f;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(GameContent.BackgroundTexture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);

            _scoreText.Message = _score.ToString();
            _scoreText.Draw(_spriteBatch);
            _gameOverText.Draw(_spriteBatch);

            foreach (IGameEntity g in _gameEntities)
            {
                g.Draw(_spriteBatch);
            }

            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}