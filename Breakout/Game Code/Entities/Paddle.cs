using Breakout.Game_Code.Interfaces;
using Breakout.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_Code.Entities
{
    public class Paddle : GameEntity, IUpdatable
    {
        private KeyboardState _oldKeyboardState;

        public Paddle()
        {
            this.UName = "Paddle";
            _oldKeyboardState = new KeyboardState();
            this.Direction = new Vector2(0, 0);
            this.Speed = 10;
            this.Texture = GameContent.PaddleTexture;
            this.Location = new Vector2((BreakoutGame.WINDOW_WIDTH / 2) - this.Texture.Width / 2, 725);
            this.IsCollidable = true;
        }

        /// <summary>
        /// Default update method for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            this.CalculateVelocity();
            this.Move();
        }

        private void CalculateVelocity()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (_oldKeyboardState.IsKeyUp(Keys.Right))
            {
                this.Direction = new Vector2(0, 0);
            }
            if (_oldKeyboardState.IsKeyUp(Keys.Left))
            {
                this.Direction = new Vector2(0, 0);
            }

            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                this.Direction = new Vector2(1, 0);
            }
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                this.Direction = new Vector2(-1, 0);
            }

            _oldKeyboardState = newKeyboardState;

            this.Velocity = Speed * Direction;
        }

        /// <summary>
        /// Moves the object.
        /// </summary>
        private void Move()
        {
            Vector2 lastLocation = this.Location;

            this.Location += Velocity;

            if (this.Location.X < 10)
            {
                this.Location = lastLocation;
            }
            if (this.Location.X > BreakoutGame.WINDOW_WIDTH - this.Texture.Width - 10)
            {
                this.Location = lastLocation;
            }
        }
    }
}