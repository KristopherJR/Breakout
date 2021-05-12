using Breakout.Game_Code.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakout.Game_Code.Entities
{
    public abstract class GameEntity : IGameEntity
    {
        #region FIELDS
        private string _uName;
        private int _uID;

        private Texture2D _texture;

        private Vector2 _location;
        private Vector2 _velocity;
        private Vector2 _direction;
        private float _speed;

        private bool _isCollidable;
        #endregion

        #region PROPERTIES
        public string UName { get => _uName; set => _uName = value; }
        public int UID { get => _uID; set => _uID = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public Vector2 Location { get => _location; set => _location = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public Vector2 Direction { get => _direction; set => _direction = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public bool IsCollidable { get => _isCollidable; set => _isCollidable = value; }

        public Rectangle HitBox
        {
            get { return new Rectangle((int)this.Location.X,
                                       (int)this.Location.Y,
                                       this.Texture.Width,
                                       this.Texture.Height); } // get method
        }

        #endregion
        /// <summary>
        /// Checks if the current GameEntity has collided with the collidee provided.
        /// </summary>
        /// <param name="collidee">The other GameEntity to check a collision with.</param>
        /// <returns>-1 = collision on left half of Paddle. 0 = no collision. 1 = collision on right half of paddle.</returns>
        public int CheckHitBoxCollision(IGameEntity collidee)
        {
            if(collidee is Paddle)
            {
                if (this.HitBox.Intersects(collidee.HitBox))
                {
                    if (this.Location.X >= collidee.Location.X && this.Location.X < collidee.Location.X + collidee.Texture.Width / 2) //left half of the paddle
                    {
                        System.Diagnostics.Debug.WriteLine("Left Half of Paddle");
                        return -1; // Collision occured on left half of paddle, Invert direction

                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Right Half of Paddle");
                        return 1; // Collision occured on right half of paddle, don't invert direction
                    }
                }
            }
            if (this.HitBox.Intersects(collidee.HitBox))
            {
                return 1; // Collision occured, Invert direction
            }
            else
            {
                return 0; // no collision
            }
        }

        /// <summary>
        /// Draws this GameEntity onto the provided SpriteBatch parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw this GameEntity onto.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White);
        }
    }
}
