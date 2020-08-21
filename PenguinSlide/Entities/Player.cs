using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Components;
using PenguinSlide.Controls;

namespace PenguinSlide.Entities
{
    public class Player : Entity, IMovable
    {
        private readonly Control control;
        private int airTime;

        private Animation idleAnimation;
        private bool isJumping, isFacingRight = true;
        private Animation jumpAnimation;
        private Animation runAnimation;
        private Animation slideAnimation;
        private Vector2 speed;
        private Vector2 velocity;

        public Player(Texture2D texture, Rectangle rectangle, Vector2 speed, float scale, Control control) : base(texture, rectangle, scale)
        {
            this.speed = speed;
            this.control = control;
        }

        public List<ICollectable> Collectables { get; } = new List<ICollectable>();
        public Vector2 Speed => speed;
        public bool CanMoveLeft { get; set; }
        public bool CanMoveRight { get; set; }
        public bool CanMoveUp { get; set; }
        public bool CanMoveDown { get; set; }

        protected override void CreateAnimations()
        {
            runAnimation = animationCreator.Create(1440, 0, 144, 128, 4);
            idleAnimation = animationCreator.Create(1440, 0, 144, 128, 1);
            jumpAnimation = animationCreator.Create(864, 0, 144, 128, 1);
            slideAnimation = animationCreator.Create(1152, 0, 144, 128, 2);
        }

        private void HandleMovement()
        {
            if (control.Right)
            {
                if (CanMoveRight) velocity.X = Speed.X;
                isFacingRight = true;
            }
            else if (control.Left)
            {
                if (CanMoveLeft) velocity.X = -Speed.X;
                isFacingRight = false;
            }

            if (control.Slide)
            {
                if (isFacingRight && CanMoveRight)
                    velocity.X = Speed.X * 1.6f;
                else if (CanMoveLeft)
                    velocity.X = -Speed.X * 1.6f;
            }

            if (control.Jump && !isJumping && !CanMoveDown)
            {
                SoundPlayer.JumpSound.Play();
                isJumping = true;
            }
            else if (!control.Jump && isJumping)
            {
                isJumping = false;
            }
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

        protected override void SetAnimation()
        {
            if (velocity.Y != 0)
                currentAnimation = jumpAnimation;
            else if (control.Slide)
                currentAnimation = slideAnimation;
            else if (control.Right || control.Left)
                currentAnimation = runAnimation;
            else
                currentAnimation = idleAnimation;
            spriteEffects = !isFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;
            velocity.Y = 0;

            if (!IsAlive) return;
            HandleJump();
            HandleGravity();
            HandleMovement();
            SetAnimation();

            Position += velocity;

            CollisionRectangle = new Rectangle((int) Position.X + (int) (25 * scale), (int) Position.Y,
                (int) ((currentAnimation.CurrentFrame.SourceRectangle.Width - 50) * scale),
                (int) (currentAnimation.CurrentFrame.SourceRectangle.Height * scale));

            currentAnimation.Update(gameTime);
        }

        public void Respawn(Vector2 position)
        {
            isFacingRight = true;
            IsAlive = true;
            currentAnimation = idleAnimation;
            Position = position;
            Collectables.Clear();
        }
    }
}