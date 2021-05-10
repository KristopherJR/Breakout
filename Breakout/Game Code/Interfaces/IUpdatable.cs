using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakout.Game_Code.Interfaces
{
    public interface IUpdatable
    {
        /// <summary>
        /// Default update method for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        void Update(GameTime gameTime);
    }
}
