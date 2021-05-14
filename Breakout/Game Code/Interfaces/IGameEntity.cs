using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Game_Code.Interfaces
{
    public interface IGameEntity
    {
        #region PROPERTIES

        public Vector2 Direction { get; set; }
        public Rectangle HitBox { get; }
        public bool IsCollidable { get; set; }
        public Vector2 Location { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public int UID { get; set; }
        public string UName { get; set; }
        public Vector2 Velocity { get; set; }
        #endregion PROPERTIES

        #region METHODS

        /// <summary>
        /// Checks if the current GameEntity has collided with the collidee provided.
        /// </summary>
        /// <param name="collidee">The other GameEntity to check a collision with.</param>
        /// <returns>-1 = collision on left half of Paddle. 0 = no collision. 1 = collision on right half of paddle.</returns>
        bool CheckHitBoxCollision(IGameEntity collidee);

        /// <summary>
        /// Draws this GameEntity onto the provided SpriteBatch parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw this GameEntity onto.</param>
        void Draw(SpriteBatch spriteBatch);
        #endregion METHODS
    }
}