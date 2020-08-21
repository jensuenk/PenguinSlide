using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Collision;
using PenguinSlide.Components;

namespace PenguinSlide.Entities
{
    public abstract class Entity : Component, ICollidable
    {
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected Animation currentAnimation;
        protected float scale;
        protected AnimationCreator animationCreator = new AnimationCreator();
        public bool IsAlive { get; set; } = true;
        public Vector2 Position { get; set; }
        protected Entity(Texture2D texture, Rectangle rectangle, float scale) : base(texture, rectangle)
        {
            CollisionRectangle = rectangle;
            this.scale = scale;
            Position = new Vector2(rectangle.X, rectangle.Y);
            CreateAnimations();
        }

        protected abstract void CreateAnimations();
        protected abstract void SetAnimation();

        public virtual void Update(GameTime gameTime)
        {
            SetAnimation();
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

        public Rectangle CollisionRectangle { get; set; }
    }
}