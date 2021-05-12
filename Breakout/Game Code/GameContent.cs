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

        public static Texture2D RedBrickTexture;
        public static Texture2D OrangeBrickTexture;
        public static Texture2D YellowBrickTexture;
        public static Texture2D GreenBrickTexture;
        public static Texture2D BlueBrickTexture;
        public static Texture2D PurpleBrickTexture;

        public static SpriteFont GameFont;

        #endregion
        public static void LoadContent(ContentManager contentManager)
        {
            BackgroundTexture = contentManager.Load<Texture2D>("background");
            PaddleTexture = contentManager.Load<Texture2D>("paddle");
            BallTexture = contentManager.Load<Texture2D>("ball");

            RedBrickTexture = contentManager.Load<Texture2D>("red_brick");
            OrangeBrickTexture = contentManager.Load<Texture2D>("orange_brick");
            YellowBrickTexture = contentManager.Load<Texture2D>("yellow_brick");
            GreenBrickTexture = contentManager.Load<Texture2D>("green_brick");
            BlueBrickTexture = contentManager.Load<Texture2D>("blue_brick");
            PurpleBrickTexture = contentManager.Load<Texture2D>("purple_brick");

            GameFont = contentManager.Load<SpriteFont>("Score");
        }
    }
}
