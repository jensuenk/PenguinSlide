using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Components;
using PenguinSlide.Controls;

namespace PenguinSlide.Entities
{
    public class Player : Entity
    {
        private readonly Animation animationDie = new Animation();
        private readonly Animation animationHurt = new Animation();
        private readonly Animation animationIdle = new Animation();
        private readonly Animation animationJump = new Animation();
        private readonly Animation animationRun = new Animation();
        private readonly Animation animationSlide = new Animation();
        private readonly Control control;
        private readonly float scale;
        private readonly Texture2D texture;
        private int airTime;

        private Animation currentAnimation;
        private bool isJumping, isFacingRight = true;
        private Vector2 speed;
        private SpriteEffects spriteEffects;
        private Vector2 velocity;

        public Player(Texture2D texture, Rectangle rectangle, Vector2 speed, float scale, Control control)
        {
            this.texture = texture;
            CollisionRectangle = rectangle;
            Position = new Vector2(rectangle.X, rectangle.Y);
            this.speed = speed;
            this.scale = scale;
            this.control = control;
            CreateAnimation();
            currentAnimation = animationIdle;
        }

        public Vector2 Position { get; set; }

        public List<ICollectable> Collectables { get; } = new List<ICollectable>();

        public bool IsAlive { get; set; } = true;

        public Vector2 Speed => speed;

        public Rectangle CollisionRectangle { get; set; }
        public bool CanMoveLeft { get; set; }
        public bool CanMoveRight { get; set; }
        public bool CanMoveUp { get; set; }
        public bool CanMoveDown { get; set; }

        private void CreateAnimation()
        {
            var animationCreator = new AnimationCreator();
            animationCreator.Create(animationRun, 1440, 0, 144, 128, 4);
            animationCreator.Create(animationIdle, 1440, 0, 144, 128, 1);
            animationCreator.Create(animationJump, 864, 0, 144, 128, 1);
            animationCreator.Create(animationSlide, 1296, 0, 144, 128, 1);
            animationCreator.Create(animationHurt, 476, 0, 144, 128, 2);
            animationCreator.Create(animationDie, 432, 0, 144, 128, 1);
        }

        private void HandleMovement()
        {
            if (control.Right)
            {
                if (CanMoveRight) velocity.X = Speed.X;
                currentAnimation = animationRun;
                isFacingRight = true;
            }
            else if (control.Left)
            {
                if (CanMoveLeft) velocity.X = -Speed.X;
                currentAnimation = animationRun;
                isFacingRight = false;
            }

            if (control.Slide)
            {
                if (isFacingRight && CanMoveRight)
                    velocity.X = Speed.X;
                else if (CanMoveLeft) velocity.X = -Speed.X;
                currentAnimation = animationSlide;
            }

            if (control.Jump && !isJumping && !CanMoveDown)
                isJumping = true;
            else if (!control.Jump && isJumping) isJumping = false;
            if (control.Idle) currentAnimation = animationIdle;

            if (velocity.Y != 0) currentAnimation = animationJump;

            spriteEffects = !isFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        private void HandleGravity()
        {
            speed.Y = 10;
            if (!isJumping && CanMoveDown)
            {
                if (airTime < 20) airTime++;

                velocity.Y += Speed.Y;
            }

            if (!CanMoveDown)
            {
                speed.Y = 0;
                airTime = 0;
            }
        }

        private void HandleJump()
        {
            speed.Y = 10;

            if (isJumping && airTime < 25)
            {
                airTime++;


                if (CanMoveUp)
                    velocity.Y -= Speed.Y;
                else
                    StopJump();
            }
            else
            {
                StopJump();
            }
        }

        private void StopJump()
        {
            airTime = 0;
            isJumping = false;
            speed.Y = 0;
            velocity.Y = 0;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;
            velocity.Y = 0;

            if (IsAlive)
            {
                HandleJump();
                HandleGravity();
                HandleMovement();
            }
            else
            {
                currentAnimation = animationDie;
            }

            Position += velocity;

            CollisionRectangle = new Rectangle((int) Position.X + (int) (25 * scale), (int) Position.Y,
                (int) ((currentAnimation.CurrentFrame.SourceRectangle.Width - 50) * scale),
                (int) (currentAnimation.CurrentFrame.SourceRectangle.Height * scale));

            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f,
                new Vector2(0, 0), scale, spriteEffects, 1);
        }

        public void Respawn(Vector2 position)
        {
            IsAlive = true;
            currentAnimation = animationIdle;
            Position = position;
            Collectables.Clear();
        }
    }
}