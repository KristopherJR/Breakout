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
        private bool _flagLifeLost;
        #endregion
        #region PROPERTIES
        public bool FlagLifeLost
        {
            get { return _flagLifeLost; }
            set { _flagLifeLost = value; }
        }
        #endregion
        public Ball()
        {
            this.UName = "Ball";
            this.Direction = new Vector2(1, 1);
            this.Speed = 2.0f;
            this.Texture = GameContent.BallTexture;
            this.Location = new Vector2((BreakoutGame.WINDOW_WIDTH / 2) - this.Texture.Width / 2, 400);
            this.FlagLifeLost = false;
            this.IsCollidable = true;
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
                this.PlayRandomCollisionSFX();
            }

            if (this.Location.X >= BreakoutGame.WINDOW_WIDTH - this.Texture.Width - 10) // Past Right
            {
                this.Direction = new Vector2(-1, this.Direction.Y);
                this.PlayRandomCollisionSFX();
            }

            if (this.Location.Y <= 10) // Past Top
            {
                this.Direction = new Vector2(this.Direction.X, 1);
                this.PlayRandomCollisionSFX();
            }

            if (this.Location.Y >= BreakoutGame.WINDOW_HEIGHT - this.Texture.Height - 10) // Past Bottom
            {
                this.Direction = new Vector2(this.Direction.X, -1);
                this.FlagLifeLost = true;
                GameContent.LifeLoss.Play();
            }

            this.Velocity = Speed * Direction;
        }

        private void CheckCollisions()
        {
            int bounceDirection = 0;

            for(int i=0; i < _gameEntities.Count; i++)
            {
                if(_gameEntities[i] != this)
                {
                    if(_gameEntities[i].IsCollidable)
                    {
                        bounceDirection = this.CheckHitBoxCollision(_gameEntities[i]);


                        if (bounceDirection == 1) // Brick collision
                        {
                            if (_gameEntities[i] is Brick)
                            {
                                this.Direction = new Vector2(this.Direction.X, -this.Direction.Y);
                                (_gameEntities[i] as Brick).FlagDeletion = true;
                                this.Speed += 0.175f;

                                this.PlayRandomCollisionSFX();
                            
                                break;
                            }
                        }

                        if (bounceDirection == 1) // hit right half of paddle
                        {
                            if (this.Direction.X > 0) // ball is heading right
                            {
                                
                                this.Direction = new Vector2(this.Direction.X, -this.Direction.Y); // continue moving right
        
                                this.PlayRandomCollisionSFX();
                                break;
                            }
                            if (this.Direction.X < 0) // ball is heading left
                            {
                                this.Direction = new Vector2(-this.Direction.X, -this.Direction.Y); // bounce back to the right
                                this.PlayRandomCollisionSFX();
                                break;
                            }
                        }

                        if (bounceDirection == -1) // hit left half of paddle
                        {
                            if (this.Direction.X > 0) // ball is heading right
                            {
                                this.Direction = new Vector2(-this.Direction.X, -this.Direction.Y); // bounce back to the left
                                this.PlayRandomCollisionSFX();
                                break;
                            }
                            if (this.Direction.X < 0) // ball is heading left
                            {
                                this.Direction = new Vector2(this.Direction.X, -this.Direction.Y); // continue moving left
                                this.PlayRandomCollisionSFX();
                                break;
                            } 
                        }
                    }
                }      
            }
        }

        private void PlayRandomCollisionSFX()
        {
            Random random = new Random();
            int randomInt = random.Next(1, 5);

            switch (randomInt)
            {
                case 1:
                    GameContent.Bounce1.Play();
                    break;
                case 2:
                    GameContent.Bounce2.Play();
                    break;
                case 3:
                    GameContent.Chime1.Play();
                    break;
                case 4:
                    GameContent.Chime2.Play();
                    break;
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
