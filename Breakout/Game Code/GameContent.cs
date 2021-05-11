using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakout.GameCode
{
    public static class GameContent
    {
        #region FIELDS
        public static Texture2D BackgroundTexture;
        public static Texture2D PaddleTexture;
        public static Texture2D BallTexture;

        #endregion
        public static void LoadContent(ContentManager contentManager)
        {
            BackgroundTexture = contentManager.Load<Texture2D>("background");
            PaddleTexture = contentManager.Load<Texture2D>("paddle");
            BallTexture = contentManager.Load<Texture2D>("ball");
        }
    }
}
