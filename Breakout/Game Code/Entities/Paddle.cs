using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Breakout.GameCode;
using Breakout.Game_Code.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Game_Code.Entities
{
    public class Paddle : GameEntity, IUpdatable
    {
        
        private KeyboardState _oldKeyboardState;
        public Paddle()
        {
            _oldKeyboardState = new KeyboardState();
            this.Direction = new Vector2(0,0);
            this.Speed = 3;
            this.Texture = GameContent.PaddleTexture;
            this.Location = new Vector2((BreakoutGame.WINDOW_WIDTH/2)-this.Texture.Width/2, 725);
        }

        private void CalculateVelocity()
        {
            KeyboardState _newKeyboardState = Keyboard.GetState();

            if (_oldKeyboardState.IsKeyUp(Keys.Right))
            {
                this.Direction = new Vector2(0, 0);
            }
            if (_oldKeyboardState.IsKeyUp(Keys.Left))
            {
                this.Direction = new Vector2(0, 0);
            }

            if (_newKeyboardState.IsKeyDown(Keys.Right))
            {
                this.Direction = new Vector2(1, 0);
            }
            if (_newKeyboardState.IsKeyDown(Keys.Left))
            {
                this.Direction = new Vector2(-1, 0);
            }

            _oldKeyboardState = _newKeyboardState;

            this.Velocity = Speed * Direction;

        }

        /// <summary>
        /// Default update method for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            this.CalculateVelocity();
            this.Location += Velocity;
        }
    }
}
