using Breakout.Game_Code.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Game_Code.Entities
{
    public abstract class GameEntity : IGameEntity
    {
        #region FIELDS

        private Vector2 _direction;
        private bool _isCollidable;
        private Vector2 _location;
        private float _speed;
        private Texture2D _texture;
        private int _uID;
        private string _uName;
        private Vector2 _velocity;
        #endregion FIELDS

        #region PROPERTIES

        public Vector2 Direction { get => _direction; set => _direction = value; }
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)this.Location.X,
                                     (int)this.Location.Y,
                                     this.Texture.Width,
                                     this.Texture.Height);
            }
        }

        public bool IsCollidable { get => _isCollidable; set => _isCollidable = value; }
        public Vector2 Location { get => _location; set => _location = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public int UID { get => _uID; set => _uID = value; }
        public string UName { get => _uName; set => _uName = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        #endregion PROPERTIES

        /// <summary>
        /// Checks if the current GameEntity has collided with the collidee provided.
        /// </summary>
        /// <param name="collidee">The other GameEntity to check a collision with.</param>
        /// <returns>-1 = collision on left half of Paddle. 0 = no collision. 1 = collision on right half of paddle.</returns>
        public bool CheckHitBoxCollision(IGameEntity collidee)
        {
            if (this.HitBox.Intersects(collidee.HitBox))
            {
                return true; // Collision occured, Invert direction
            }
            else
            {
                return false; // no collision
            }
        }

        /// <summary>
        /// Draws this GameEntity onto the provided SpriteBatch parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw this GameEntity onto.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White);
        }
    }
}