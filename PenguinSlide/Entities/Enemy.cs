using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Components;

namespace PenguinSlide.Entities
{
    public class Enemy : Entity, IDamageable
    {
        private readonly Vector2 beginPosition;
        private readonly Vector2 endPosition;
        private Animation idleAnimation;

        public Enemy(Texture2D texture, Rectangle rectangle, Vector2 speed, float scale, Vector2 endPosition) : base(
            texture, rectangle, speed, scale)
        {
            this.endPosition = endPosition;
            beginPosition = Position;
        }

        protected override void CreateAnimations()
        {
            idleAnimation = AnimationCreator.Create(0, 0, 128, 132, 2);
        }

        protected override void SetAnimation()
        {
            CurrentAnimation = idleAnimation;
            SpriteEffects = !IsFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        protected override void HandleMovement()
        {
            if (IsFacingRight)
                Velocity.X = Speed.X;
            else
                Velocity.X = -Speed.X;

            if (Position.X >= endPosition.X && IsFacingRight)
                IsFacingRight = false;
            else if (Position.X <= beginPosition.X && !IsFacingRight) IsFacingRight = true;
        }
    }
}