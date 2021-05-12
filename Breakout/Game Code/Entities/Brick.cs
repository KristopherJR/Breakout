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
            if (this.UID > 20 && this.UID <= 30)
            {
                this.Texture = GameContent.YellowBrickTexture;
            }
            if (this.UID > 30 && this.UID <= 40)
            {
                this.Texture = GameContent.GreenBrickTexture;
            }
            if (this.UID > 40 && this.UID <= 50)
            {
                this.Texture = GameContent.BlueBrickTexture;
            }
            if (this.UID > 50 && this.UID <= 60)
            {
                this.Texture = GameContent.PurpleBrickTexture;
            }
        }
    }
}
