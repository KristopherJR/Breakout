using Breakout.Game_Code.Interfaces;
using Breakout.GameCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakout.Game_Code.Entities
{
    public class Ball : GameEntity, IUpdatable
    {
        #region FIELDS
        private List<IGameEntity> _gameEntities;
        #endregion
        public Ball()
        {
            this.UName = "Ball";
            this.Direction = new Vector2(1, 1);
            this.Speed = 2;
            this.Texture = GameContent.BallTexture;
            this.Location = new Vector2((BreakoutGame.WINDOW_WIDTH / 2) - this.Texture.Width / 2, 400);
        }

        public void PopulateCollidables(List<IGameEntity> gameEntities)
        {
            _gameEntities = gameEntities;
        }

        private void CalculateVelocity()
        {
            if (this.Location.X <= 10) // Past Left
            {
                this.Direction = new Vector2(1, this.Direction.Y);
            }

            if (this.Location.X >= BreakoutGame.WINDOW_WIDTH - this.Texture.Width - 10) // Past Right
            {
                this.Direction = new Vector2(-1, this.Direction.Y);
            }

            if (this.Location.Y <= 10) // Past Top
            {
                this.Direction = new Vector2(this.Direction.X, 1);
            }

            if (this.Location.Y >= BreakoutGame.WINDOW_HEIGHT - this.Texture.Height - 10) // Past Bottom
            {
                this.Direction = new Vector2(this.Direction.X, -1);
            }

            this.Velocity = Speed * Direction;
        }

        private void CheckCollisions()
        {
            foreach(IGameEntity g in _gameEntities)
            {
                if(g != this)
                {
                    if (this.CheckHitBoxCollision(g))
                    {
                        this.Direction = new Vector2(this.Direction.X, -this.Direction.Y);
                    }
                }      
            }
        }

        /// <summary>
        /// Moves the object.
        /// </summary>
        private void Move()
        {
            this.Location += Velocity;
        }

        /// <summary>
        /// Default update method for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            this.CalculateVelocity();  
            this.Move();
            this.CheckCollisions();
        }
    }
}
