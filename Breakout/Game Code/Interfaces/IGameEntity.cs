using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Text;

namespace Breakout.Game_Code.Interfaces
{
    public interface IGameEntity
    {
        #region PROPERTIES
        public string UName { get; }
        public string UID { get; }
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Direction { get; set; }
        public int Speed { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Draws this GameEntity onto the provided SpriteBatch parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw this GameEntity onto.</param>
        void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
