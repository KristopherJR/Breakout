using Breakout.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Game_Code.Entities
{
    public class Text : GameEntity
    {
        #region FIELDS

        private SpriteFont _font;
        private Color _fontColor;
        private string _message;
        private Vector2 _messageDimensions;
        #endregion FIELDS

        #region PROPERTIES

        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; _messageDimensions = _font.MeasureString(_message); }
        }

        public Color FontColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; _messageDimensions = _font.MeasureString(_message); }
        }

        public Vector2 MessageDimensions
        {
            get { return _messageDimensions; }
        }
        #endregion PROPERTIES

        /// <summary>
        /// Constructor for objects of class Text.
        /// </summary>
        /// <param name="location">The Location to place the text on the screen.</param>
        /// <param name="message">The message that should be displayed.</param>
        public Text(Vector2 location, string message)
        {
            this.Location = location;
            this.IsCollidable = false;

            _font = GameContent.GameFont26;
            _fontColor = Color.White;
            _message = message;
            _messageDimensions = _font.MeasureString(_message);
        }

        /// <summary>
        /// Centres the text in the centre of the window when this method is called.
        /// </summary>
        public void CentreOnScreen()
        {
            this.Location = new Vector2((BreakoutGame.WINDOW_WIDTH / 2) - _messageDimensions.X / 2,
                                        (BreakoutGame.WINDOW_HEIGHT / 2) - _messageDimensions.Y / 2);
        }

        /// <summary>
        /// Draws the text onto the screen.
        /// </summary>
        /// <param name="spriteBatch">A reference to the SpriteBatch to draw the Text onto.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _message, this.Location, _fontColor);
        }
    }
}