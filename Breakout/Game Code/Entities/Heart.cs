using Breakout.GameCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakout.Game_Code.Entities
{
    public class Heart : GameEntity
    {
        public Heart()
        {
            this.UName = "Heart";
            this.Texture = GameContent.HeartTexture;
            this.IsCollidable = false;
        }
    }
}
