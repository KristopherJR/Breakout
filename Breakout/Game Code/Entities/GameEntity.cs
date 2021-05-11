﻿using Breakout.Game_Code.Interfaces;
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
        private int _speed;
        #endregion

        #region PROPERTIES
        public string UName { get => _uName; set => _uName = value; }
        public int UID { get => _uID; set => _uID = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public Vector2 Location { get => _location; set => _location = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public Vector2 Direction { get => _direction; set => _direction = value; }
        public int Speed { get => _speed; set => _speed = value; }

        public Rectangle HitBox
        {
            get { return new Rectangle((int)this.Location.X,
                                       (int)this.Location.Y,
                                       this.Texture.Width,
                                       this.Texture.Height); } // get method
        }

        #endregion
        /// <summary>
        /// Checks if the current IGameEntity has collided with the collidee provided.
        /// </summary>
        /// <param name="collidee">The other IGameEntity to check a collision with.</param>
        /// <returns>True if there's a collision, else false.</returns>
        public bool CheckHitBoxCollision(IGameEntity collidee)
        {
            if(this.HitBox.Intersects(collidee.HitBox))
            {
                return true;
            }
            else
            {
                return false;
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
