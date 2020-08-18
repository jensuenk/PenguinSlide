﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide
{
    public class Player : Entity, ICollidable, IControllable
    {
        private Texture2D texture;
        private Control control;
        private Vector2 position;
        public Vector2 Speed;
        private float scale;

        public Rectangle CollisionRectangle { get; set; }
        private Animation currentAnimation;
        private SpriteEffects spriteEffects;
        private Vector2 velocity;
        private bool isJumping, isFacingRight;
        public bool CanMoveLeft, CanMoveRight, CanMoveUp, CanMoveDown;

        Animation animationRun = new Animation();
        Animation animationIdle = new Animation();
        Animation animationJumpIn = new Animation();
        Animation animationJump = new Animation();
        Animation animationSlideIn = new Animation();
        Animation animationSlide = new Animation();
        Animation animationHurt = new Animation();
        Animation animationDie = new Animation();

        public Player(Texture2D texture, Rectangle rectangle, Vector2 position, Vector2 speed, float scale, Control control)
        {
            this.texture = texture;
            this.CollisionRectangle = rectangle;
            this.position = position;
            this.Speed = speed;
            this.scale = scale;
            this.control = control;
            CreateAnimation();
        }

        private void CreateAnimation()
        {
            AnimationCreator animationCreator = new AnimationCreator();
            animationCreator.Create(animationRun, 1440, 0, 144, 128, 4);
            animationCreator.Create(animationIdle, 1440, 0, 144, 128, 4);
            animationCreator.Create(animationJumpIn, 720, 0, 144, 128, 1);
            animationCreator.Create(animationJump, 864, 0, 144, 128, 1);
            animationCreator.Create(animationSlideIn, 1152, 0, 144, 128, 1);
            animationCreator.Create(animationSlide, 1296, 0, 144, 128, 1);
            animationCreator.Create(animationHurt, 576, 0, 144, 128, 2);
            animationCreator.Create(animationDie, 0, 0, 144, 128, 4);
        }

        private void Move(GameTime gameTime)
        {
            if (control.Right)
            {
                if (CanMoveRight)
                {
                    velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                }
                currentAnimation = animationRun;
                isFacingRight = true;
            }
            else if (control.Left)
            {
                if (CanMoveLeft)
                {
                    velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                }
                currentAnimation = animationRun;
                isFacingRight = false;
            }
            else velocity.X = 0f;
            
            if (control.Slide)
            {
                if (isFacingRight && CanMoveRight)
                {
                    velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                }
                else if (CanMoveLeft)
                {
                    velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                    
                }
                currentAnimation = animationSlide;
            }
            if (control.Jump && CanMoveUp && !isJumping)
            {
                velocity.Y = -20f;
                isJumping = true;
                currentAnimation = animationJump;
            }
            if (control.Idle)
            {
                currentAnimation = animationIdle;
            }
            if (!isFacingRight)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            position += velocity;

            /*

            if (velocity.Y < 10)
            {
                if (CanMoveDown)
                {
                    velocity.Y += 0.6f;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (velocity.Y != 0)
            {
                currentAnimation = animationJump;
            }
            */

            CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, currentAnimation.CurrentFrame.SourceRectangle.Width, currentAnimation.CurrentFrame.SourceRectangle.Height);
            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffects, 1);
        }
    }
}
