using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Components;
using PenguinSlide.Controls;
using PenguinSlide.Sound;

namespace PenguinSlide.Entities
{
    public class Player : Entity
    {
        private readonly Control control;
        private int airTime;

        private Animation idleAnimation;
        private bool isJumping;
        private Animation jumpAnimation;
        private Animation runAnimation;
        private Animation slideAnimation;

        public Player(Texture2D texture, Rectangle rectangle, Vector2 speed, float scale, Control control) : base(
            texture, rectangle, speed, scale)
        {
            this.control = control;
        }

        public List<ICollectable> Collectables { get; } = new List<ICollectable>();
        public bool CanMoveLeft { get; set; }
        public bool CanMoveRight { get; set; }
        public bool CanMoveUp { get; set; }
        public bool CanMoveDown { get; set; }

        protected override void CreateAnimations()
        {
            runAnimation = AnimationCreator.Create(1440, 0, 144, 128, 4);
            idleAnimation = AnimationCreator.Create(1440, 0, 144, 128, 1);
            jumpAnimation = AnimationCreator.Create(864, 0, 144, 128, 1);
            slideAnimation = AnimationCreator.Create(1152, 0, 144, 128, 2);
        }

        protected override void HandleMovement()
        {
            Velocity.X = 0;
            Velocity.Y = 0;

            if (control.Right)
            {
                if (CanMoveRight) Velocity.X = Speed.X;
                IsFacingRight = true;
            }
            else if (control.Left)
            {
                if (CanMoveLeft) Velocity.X = -Speed.X;
                IsFacingRight = false;
            }

            if (control.Slide)
            {
                if (IsFacingRight && CanMoveRight)
                    Velocity.X = Speed.X * 1.6f;
                else if (CanMoveLeft)
                    Velocity.X = -Speed.X * 1.6f;
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
            Speed.Y = 10;
            if (!isJumping && CanMoveDown)
            {
                if (airTime < 20) airTime++;
                Velocity.Y += Speed.Y;
            }

            if (!CanMoveDown)
            {
                Speed.Y = 0;
                airTime = 0;
            }
        }

        private void HandleJump()
        {
            Speed.Y = 10;
            if (isJumping && airTime < 25)
            {
                airTime++;
                if (CanMoveUp)
                    Velocity.Y -= Speed.Y;
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
            Speed.Y = 0;
            Velocity.Y = 0;
        }

        protected override void SetAnimation()
        {
            if (Velocity.Y != 0)
                CurrentAnimation = jumpAnimation;
            else if (control.Slide)
                CurrentAnimation = slideAnimation;
            else if (control.Right || control.Left)
                CurrentAnimation = runAnimation;
            else
                CurrentAnimation = idleAnimation;
            SpriteEffects = !IsFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive) return;
            HandleMovement();
            HandleJump();
            HandleGravity();
            SetAnimation();

            Position += Velocity;

            CollisionRectangle = new Rectangle((int) Position.X + (int) (25 * Scale), (int) Position.Y,
                (int) ((CurrentAnimation.CurrentFrame.SourceRectangle.Width - 50) * Scale),
                (int) (CurrentAnimation.CurrentFrame.SourceRectangle.Height * Scale));

            CurrentAnimation.Update(gameTime);
        }

        public void Respawn(Vector2 position)
        {
            IsFacingRight = true;
            IsAlive = true;
            CurrentAnimation = idleAnimation;
            Position = position;
            Collectables.Clear();
        }
    }
}