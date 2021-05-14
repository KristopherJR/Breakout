using Breakout.GameCode;

namespace Breakout.Game_Code.Entities
{
    public class Brick : GameEntity
    {
        #region FIELDS

        private bool _flagDeletion;
        private int _scoreValue;

        #endregion FIELDS

        #region PROPERTIES

        public bool FlagDeletion
        {
            get { return _flagDeletion; }
            set { _flagDeletion = value; }
        }

        public int ScoreValue
        {
            get { return _scoreValue; }
            set { _scoreValue = value; }
        }

        #endregion PROPERTIES

        public Brick()
        {
            this.UName = "Brick";
            _flagDeletion = false;
            this.IsCollidable = true;
        }

        /// <summary>
        /// Gives each Brick a different colour depending on it's ID number. Different coloured bricks also offer different score values.
        /// </summary>
        public void SetTexture()
        {
            if (this.UID <= 10)
            {
                this.Texture = GameContent.RedBrickTexture;
                this.ScoreValue = 10;
            }
            if (this.UID > 10 && this.UID <= 20)
            {
                this.Texture = GameContent.OrangeBrickTexture;
                this.ScoreValue = 20;
            }
            if (this.UID > 20 && this.UID <= 30)
            {
                this.Texture = GameContent.YellowBrickTexture;
                this.ScoreValue = 30;
            }
            if (this.UID > 30 && this.UID <= 40)
            {
                this.Texture = GameContent.GreenBrickTexture;
                this.ScoreValue = 40;
            }
            if (this.UID > 40 && this.UID <= 50)
            {
                this.Texture = GameContent.BlueBrickTexture;
                this.ScoreValue = 50;
            }
            if (this.UID > 50 && this.UID <= 60)
            {
                this.Texture = GameContent.PurpleBrickTexture;
                this.ScoreValue = 60;
            }
        }
    }
}