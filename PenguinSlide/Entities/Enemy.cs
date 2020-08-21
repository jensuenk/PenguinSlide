using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Components;

namespace PenguinSlide.Entities
{
    public class Enemy : Entity, IDamageable
    {
        private Animation idleAnimation;
        
        public Enemy(Texture2D texture, Rectangle rectangle, float scale) : base(texture, rectangle, scale)
        {
        }

        protected override void CreateAnimations()
        {
            idleAnimation = animationCreator.Create(0, 0, 128, 132, 2);
        }

        protected override void SetAnimation()
        {
            currentAnimation = idleAnimation;
        }
    }
}