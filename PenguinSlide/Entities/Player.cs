using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Controls;

namespace PenguinSlide.Entities
{
    public class Player : Entity, IMovable
    {
        private Texture2D texture;
        private Control control;
        private Vector2 position;
        private Vector2 speed;
        public Vector2 Speed => speed;
        private float scale;
        private int airTime;
        public Rectangle CollisionRectangle { get; set; }
        public bool CanMoveLeft { get; set; }
        public bool CanMoveRight { get; set; }
        public bool CanMoveUp { get; set; }
        public bool CanMoveDown { get; set; }
        
        private Animation currentAnimation;
        private SpriteEffects spriteEffects;
        private Vector2 velocity;
        private bool isJumping, isFacingRight;

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
            this.speed = speed;
            this.scale = scale;
            this.control = control;
            CreateAnimation();
        }
        private void CreateAnimation()
        {
            AnimationCreator animationCreator = new AnimationCreator();
            animationCreator.Create(animationRun, 1440, 0, 144, 128, 4);
            animationCreator.Create(animationIdle, 1440, 0, 144, 128, 1);
            animationCreator.Create(animationJumpIn, 720, 0, 144, 128, 1);
            animationCreator.Create(animationJump, 864, 0, 144, 128, 1);
            animationCreator.Create(animationSlideIn, 1152, 0, 144, 128, 1);
            animationCreator.Create(animationSlide, 1296, 0, 144, 128, 1);
            animationCreator.Create(animationHurt, 576, 0, 144, 128, 2);
            animationCreator.Create(animationDie, 0, 0, 144, 128, 4);
        }
        private void HandleMovement()
        {
            if (control.Right)
            {
                if (CanMoveRight)
                {
                    velocity.X = Speed.X;
                }
                currentAnimation = animationRun;
                isFacingRight = true;
            }
            else if (control.Left)
            {
                if (CanMoveLeft)
                {
                    velocity.X = -Speed.X;
                }
                currentAnimation = animationRun;
                isFacingRight = false;
            }
            if (control.Slide)
            {
                if (isFacingRight && CanMoveRight)
                {
                    velocity.X = Speed.X;
                }
                else if (CanMoveLeft)
                {
                    velocity.X = -Speed.X;
                }
                currentAnimation = animationSlide;
            }
            if (control.Jump && isJumping == false && !CanMoveDown)
            {
                isJumping = true;
            }
            if (control.Idle)
            {
                currentAnimation = animationIdle;
            }
            
            if (velocity.Y != 0)
            {
                currentAnimation = animationJump;
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
        public void HandleGravity()
        {
            speed.Y = 10;
            if (!isJumping && CanMoveDown)
            {

                if (airTime < 20)
                {
                    airTime++;
                }

                velocity.Y += Speed.Y;
            }

            if (!CanMoveDown)
            {
                speed.Y = 0;
                airTime = 0;
            }
        }
        public void HandleJump()
        {
            speed.Y = 10;

            if (isJumping && airTime < 25)
            {
                airTime++;


                if (CanMoveUp)
                {
                    velocity.Y -= Speed.Y;
                }
                else
                {
                    StopJump();
                }
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
            
            HandleJump();
            HandleGravity();
            HandleMovement();

            position += velocity;
            CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)((currentAnimation.CurrentFrame.SourceRectangle.Width - 10) * scale), (int)(currentAnimation.CurrentFrame.SourceRectangle.Height * scale));
            
            currentAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), scale, spriteEffects, 1);
        }

    }
}
