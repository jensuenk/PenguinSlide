using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Collision;
using PenguinSlide.Components;
using PenguinSlide.Controls;

namespace PenguinSlide.Entities
{
    public class Player : Entity, IMovable
    {
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation runAnimation;
        private Animation slideAnimation;
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
            currentAnimation = idleAnimation;
        }

        public List<ICollectable> Collectables { get; } = new List<ICollectable>();

        public Vector2 Speed => speed;

        private void CreateAnimation()
        {
            var animationCreator = new AnimationCreator();
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
            else if (!control.Jump && isJumping) isJumping = false;
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

        private void SetAnimations()
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
            SetAnimations();

            Position += velocity;

            CollisionRectangle = new Rectangle((int) Position.X + (int) (25 * scale), (int) Position.Y,
                (int) ((currentAnimation.CurrentFrame.SourceRectangle.Width -  50)* scale),
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
            currentAnimation = idleAnimation;
            Position = position;
            Collectables.Clear();
        }
    }
}