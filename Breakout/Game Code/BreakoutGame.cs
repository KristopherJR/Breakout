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

        public const int BRICK_COLUMNS = 10;
        public const int BRICK_ROWS = 6;
        public const int WINDOW_HEIGHT = 800;
        public const int WINDOW_WIDTH = 1200;
        private Ball _ball;
        private float _flashInterval;
        private float _flashTimer;
        private bool _fontGreen;
        private bool _fontRed;
        private List<IGameEntity> _gameEntities;
        private Text _gameOverText;
        private GraphicsDeviceManager _graphics;
        private float _idleTimer;
        private int _lives;
        private Paddle _paddle;
        private bool _running;
        private int _score;
        private Text _scoreText;
        private bool _scriptStop;
        private float _speechWaitDuration;
        private SpriteBatch _spriteBatch;
        private bool _win;

        #endregion FIELDS

        /// <summary>
        /// Constructor for BreakoutGame
        /// </summary>
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
            _fontGreen = false;
            _fontRed = false;
            _win = false;
        }

        /// <summary>
        /// Draws all objects onto the screen.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
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

        /// <summary>
        /// Initialises all game content.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();
        }

        /// <summary>
        /// Loads all game content.
        /// </summary>
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
        /// Update method for BreakoutGame.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_running)
            {
                CheckObjectState(gameTime);

                base.Update(gameTime);

                if (!_gameEntities.OfType<Brick>().Any()) // if all bricks have been broken, player wins
                {
                    GameWon();
                }

                if (_lives == 0)
                {
                    GameLost();
                }
            }

            if (!_running && !_scriptStop) // if the game isn't running but script stop isnt active
            {
                if (_idleTimer < _speechWaitDuration) // wait 5 seconds
                {
                    _idleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (_idleTimer >= _speechWaitDuration)
                {
                    _scriptStop = true;
                    GameContent.GameOverVoice.Play(); // play the game over speech
                }
            }

            if (!_running)
            {
                ShowGameOverText(gameTime);
            }
        }

        /// <summary>
        /// Checks to see if the player lost a life or if a brick needs to be deleted from the game.
        /// </summary>
        /// <param name="gameTime"></param>
        private void CheckObjectState(GameTime gameTime)
        {
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
        }

        /// <summary>
        /// Sets up game state for when the player has lost.
        /// </summary>
        private void GameLost()
        {
            _ball.Location = new Vector2(WINDOW_WIDTH / 2 - _ball.Texture.Width / 2, 675);
            _running = false;
            _fontRed = true;
            _win = false;
            _gameOverText.CentreOnScreen();
            GameContent.GameOverTrumpets.Play();
        }

        /// <summary>
        /// Sets up game state for when the player has won.
        /// </summary>
        private void GameWon()
        {
            _gameOverText.Message = "YOU WIN!";
            _fontGreen = true;
            _win = true;
            _gameOverText.CentreOnScreen();
            GameContent.Victory.Play();
            _running = false;
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

        /// <summary>
        /// Displays appropriate game over text when the player has won/lost.
        /// </summary>
        /// <param name="gameTime"></param>
        private void ShowGameOverText(GameTime gameTime)
        {
            if (_flashTimer < _flashInterval) // wait for the total duration
            {
                _flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_flashTimer >= _flashInterval) // time to change colour
            {
                if (_fontRed == true && !_win) // change text to red
                {
                    _gameOverText.FontColor = Color.Crimson;
                    _fontRed = false;
                }
                else if (_fontGreen == true && _win) // change text to green
                {
                    _gameOverText.FontColor = Color.LawnGreen;
                    _fontGreen = false;
                }
                else // else change it to white
                {
                    _gameOverText.FontColor = Color.White;
                    _fontRed = true;
                    _fontGreen = true;
                }
                _flashTimer = 0.0f; // reset timer
            }
        }
    }
}