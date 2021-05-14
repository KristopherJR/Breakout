using Breakout.GameCode;

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