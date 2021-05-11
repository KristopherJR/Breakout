using Breakout.GameCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakout.Game_Code.Entities
{
    public class Brick : GameEntity
    {
        public Brick()
        {
            this.UName = "Brick";
            
        }

        public void SetTexture()
        {
            if(this.UID <= 10)
            {
                this.Texture = GameContent.RedBrickTexture;
            }
            if (this.UID > 10 && this.UID <= 20)
            {
                this.Texture = GameContent.OrangeBrickTexture;
            }

        }
    }
}
