using Microsoft.Xna.Framework.Audio;
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

        public static Texture2D HeartTexture;

        public static SpriteFont GameFont;

        public static SoundEffect Bounce1;
        public static SoundEffect Bounce2;
        public static SoundEffect Chime1;
        public static SoundEffect Chime2;
        public static SoundEffect GameOverTrumpets;
        public static SoundEffect GameOverVoice;
        public static SoundEffect LifeLoss;
        public static SoundEffect Victory;

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

            HeartTexture = contentManager.Load<Texture2D>("heart");

            GameFont = contentManager.Load<SpriteFont>("Score");

            Bounce1 = contentManager.Load<SoundEffect>("bounce_1");
            Bounce2 = contentManager.Load<SoundEffect>("bounce_2");
            Chime1 = contentManager.Load<SoundEffect>("chime_1");
            Chime2 = contentManager.Load<SoundEffect>("chime_2");
            GameOverTrumpets = contentManager.Load<SoundEffect>("game_over_trumpets");
            GameOverVoice = contentManager.Load<SoundEffect>("game_over_voice");
            LifeLoss = contentManager.Load<SoundEffect>("life_loss");
            Victory = contentManager.Load<SoundEffect>("victory");
        }
    }
}
