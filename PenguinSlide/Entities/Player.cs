﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide
{
    class Player : Entity, ICollidable, IControllable
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Control control;
        private Vector2 position;
        private float scale;

        public Rectangle CollisionRectangle { get; set; }
        private Animation currentAnimation;
        private SpriteEffects spriteEffects;
        private Vector2 velocity;
        private bool isJumping, isFacingRight, isSliding;

        Animation animationRun = new Animation();
        Animation animationIdle = new Animation();
        Animation animationJumpIn = new Animation();
        Animation animationJump = new Animation();
        Animation animationSlideIn = new Animation();
        Animation animationSlide = new Animation();
        Animation animationHurt = new Animation();
        Animation animationDie = new Animation();

        public Player(Texture2D texture, Rectangle rectangle, Vector2 position, float scale, Control control)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.position = position;
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

        private void GetInputs(GameTime gameTime)
        {
            if (control.Right)
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;

                currentAnimation = animationRun;
                isFacingRight = true;
            }
            else if (control.Left)
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                
                currentAnimation = animationRun;
                isFacingRight = false;
            }
            else velocity.X = 0f;
            
            if (control.Slide)
            {
                currentAnimation = animationSlide;
                if (!isSliding)
                {
                    currentAnimation = animationSlideIn;
                    isSliding = true;
                }
                if (isFacingRight)
                {
                    velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                }
                else
                {
                    velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                }
            }
            else
            {
                isSliding = false;
            }

            if (control.Jump)
            {
                currentAnimation = animationJump;
                if (!isJumping)
                {
                    position.Y -= 5f;
                    velocity.Y = -20f;
                    currentAnimation = animationJumpIn;
                    isJumping = true;
                }
            }


            if (control.Idle)
            {
                currentAnimation = animationIdle;
            }
        }

        public override void Update(GameTime gameTime)
        {
            GetInputs(gameTime);

            position += velocity;

            if (!isFacingRight)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }

            if (velocity.Y < 10)
            {
                velocity.Y += 0.6f;
            }
            if (position.Y > 500)
            {
                velocity.Y = 0;
                isJumping = false;
            }
            if (velocity.Y != 0)
            {
                currentAnimation = animationJump;
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, currentAnimation.CurrentFrame.SourceRectangle.Width, currentAnimation.CurrentFrame.SourceRectangle.Height);

            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffects, 1);
        }
    }
}