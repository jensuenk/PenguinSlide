using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Animations;
using PenguinSlide.Collision;
using PenguinSlide.Components;

namespace PenguinSlide.Entities
{
    public abstract class Entity : Component, ICollidable
    {
        protected readonly AnimationCreator AnimationCreator = new AnimationCreator();
        protected readonly float Scale;
        protected Animation CurrentAnimation;
        protected bool IsFacingRight = true;
        public Vector2 Speed;
        protected SpriteEffects SpriteEffects = SpriteEffects.None;
        protected Vector2 Velocity;

        protected Entity(Texture2D texture, Rectangle rectangle, Vector2 speed, float scale) : base(texture, rectangle)
        {
            CollisionRectangle = rectangle;
            Scale = scale;
            Speed = speed;
            Position = new Vector2(rectangle.X, rectangle.Y);
            CreateAnimations();
        }

        public bool IsAlive { get; set; } = true;
        public Vector2 Position { get; set; }

        public Rectangle CollisionRectangle { get; set; }

        protected abstract void CreateAnimations();
        protected abstract void SetAnimation();
        protected abstract void HandleMovement();

        public virtual void Update(GameTime gameTime)
        {
            if (!IsAlive) return;
            HandleMovement();
            SetAnimation();

            Position += Velocity;

            CollisionRectangle = new Rectangle((int) Position.X, (int) Position.Y,
                (int) (CurrentAnimation.CurrentFrame.SourceRectangle.Width * Scale),
                (int) (CurrentAnimation.CurrentFrame.SourceRectangle.Height * Scale));

            CurrentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) return;
            spriteBatch.Draw(Texture, Position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f,
                new Vector2(0, 0), Scale, SpriteEffects, 1);
        }
    }
}