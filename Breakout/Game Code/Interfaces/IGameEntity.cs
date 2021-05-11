using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Text;
using Breakout.Game_Code.Entities;

namespace Breakout.Game_Code.Interfaces
{
    public interface IGameEntity
    {
        #region PROPERTIES
        public string UName { get; set; }
        public int UID { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Direction { get; set; }
        public int Speed { get; set; }
        public Rectangle HitBox { get; }
        
        #endregion

        #region METHODS
        /// <summary>
        /// Draws this GameEntity onto the provided SpriteBatch parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw this GameEntity onto.</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Checks if the current GameEntity has collided with the collidee provided.
        /// </summary>
        /// <param name="collidee">The other GameEntity to check a collision with.</param>
        /// <returns>True if there's a collision, else false.</returns>
        bool CheckHitBoxCollision(IGameEntity collidee);
        #endregion
    }
}
