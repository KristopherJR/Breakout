using Breakout.Game_Code.Interfaces;
using Breakout.GameCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Breakout.Game_Code.Entities
{
    public class Ball : GameEntity, IUpdatable
    {
        #region FIELDS

        private List<IGameEntity> _collidableEntities;
        private bool _flagLifeLost;

        #endregion FIELDS

        #region PROPERTIES

        public bool FlagLifeLost
        {
            get { return _flagLifeLost; }
            set { _flagLifeLost = value; }
        }

        #endregion PROPERTIES

        public Ball()
        {
            _collidableEntities = new List<IGameEntity>();
            _flagLifeLost = false;

            this.UName = "Ball";
            this.Direction = new Vector2(1, 1);
            this.Speed = 3.0f;
            this.Velocity = Speed * Direction;
            this.Texture = GameContent.BallTexture;
            this.Location = new Vector2((BreakoutGame.WINDOW_WIDTH / 2) - this.Texture.Width / 2, 400);
            this.IsCollidable = true;
        }

        /// <summary>
        /// Gives the Ball a list of all of the Collidable GameEntities so it can check for collisions.
        /// </summary>
        /// <param name="gameEntities">A list of all the GameEntity objects.</param>
        public void PopulateCollidables(List<IGameEntity> gameEntities)
        {
            foreach (IGameEntity g in gameEntities)
            {
                if (g.IsCollidable)
                {
                    _collidableEntities.Add(g);
                }
            }
        }

        /// <summary>
        /// Default update method for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            // check for collisions first
            this.CheckWallBounce();
            this.CheckObjectCollisions();
            // then move the ball
            this.Move();
        }

        /// <summary>
        /// This method allows the player to add spin to the ball. If the Paddle is in motion at the time of collision with the ball, it'll affect the Balls velocity.
        /// </summary>
        /// <param name="paddle"></param>
        private void CalculateBounceAngle(IGameEntity paddle)
        {
            this.Direction = new Vector2(this.Direction.X + (paddle.Direction.X * 0.6f),
                                         -this.Direction.Y);
            this.Velocity = Speed * Direction;
        }

        /// <summary>
        /// This method checks for object collision with the Ball (Paddles and Bricks) and allows the Ball to react accordingly.
        /// </summary>
        private void CheckObjectCollisions()
        {
            for (int i = 0; i < _collidableEntities.Count; i++)
            {
                if (_collidableEntities[i] != this) // if object isn't a Ball
                {
                    if (_collidableEntities[i] is Paddle)
                    {
                        if (this.CheckHitBoxCollision(_collidableEntities[i])) // check if the ball hit the paddle
                        {
                            this.CalculateBounceAngle(_collidableEntities[i]); // calculate the angle that the ball should reflect off the paddle
                            break;
                        }
                    }

                    if (_collidableEntities[i] is Brick)
                    {
                        if (this.CheckHitBoxCollision(_collidableEntities[i]))
                        {
                            if (this.Direction.Y < 0) // if the ball was heading upwards
                            {
                                this.Direction = new Vector2(this.Direction.X, 1); // make it bounce downwards
                            }
                            else // else the ball was moving downwards
                            {
                                this.Direction = new Vector2(this.Direction.X, -1); // make it bounce upwards
                            }

                            (_collidableEntities[i] as Brick).FlagDeletion = true;
                            _collidableEntities.RemoveAt(i);
                            this.Speed += 0.175f;
                            this.PlayRandomCollisionSFX();
                            this.Velocity = Speed * Direction;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method will check if the ball has hit the wall. If it has, it will adjust it's direction accordingly, set it's new velocity and play a collision sound effect.
        /// </summary>
        private void CheckWallBounce()
        {
            if (this.Location.X <= 10) // Past Left
            {
                this.Direction = new Vector2(1, this.Direction.Y);
                this.PlayRandomCollisionSFX();
                this.Velocity = Speed * Direction;
            }

            if (this.Location.X >= BreakoutGame.WINDOW_WIDTH - this.Texture.Width - 10) // Past Right
            {
                this.Direction = new Vector2(-1, this.Direction.Y);
                this.PlayRandomCollisionSFX();
                this.Velocity = Speed * Direction;
            }

            if (this.Location.Y <= 10) // Past Top
            {
                this.Direction = new Vector2(this.Direction.X, 1);
                this.PlayRandomCollisionSFX();
                this.Velocity = Speed * Direction;
            }

            if (this.Location.Y >= BreakoutGame.WINDOW_HEIGHT - this.Texture.Height - 10) // Past Bottom
            {
                this.Direction = new Vector2(this.Direction.X, -1);
                this.FlagLifeLost = true;
                GameContent.LifeLoss.Play();

                this.Location = new Vector2(600 - this.Texture.Width/2, 400);
                this.Velocity = Speed * Direction;
            }
        }
        /// <summary>
        /// Moves the ball.
        /// </summary>
        private void Move()
        {
            this.Location += this.Velocity;
        }

        /// <summary>
        /// Plays a random collision sound effect from the available effects when called.
        /// </summary>
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
    }
}