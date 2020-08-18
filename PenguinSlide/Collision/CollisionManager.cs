using Microsoft.Xna.Framework;
using PenguinSlide.Entities;
using PenguinSlide.LevelComponents;

namespace PenguinSlide.Collision
{
    public class CollisionManager
    {
        private readonly Level level;
        private readonly IMovable movable;

        public CollisionManager(IMovable movable, Level level)
        {
            this.movable = movable;
            this.level = level;
        }

        public void UpdateCollision()
        {
            var canMoveLeft = true;
            var canMoveUp = true;
            var canMoveRight = true;
            var canMoveDown = true;

            foreach (var tile in level.Components)
            {
                if (tile is ICollectable || tile is IDamageable)
                    continue;
                if (canMoveLeft)
                    canMoveLeft = !IsTouchingRight(tile.CollisionRectangle);
                if (canMoveUp)
                    canMoveUp = !IsTouchingBottom(tile.CollisionRectangle);
                if (canMoveRight)
                    canMoveRight = !IsTouchingLeft(tile.CollisionRectangle);
                if (canMoveDown)
                    canMoveDown = !IsTouchingTop(tile.CollisionRectangle);
            }

            movable.CanMoveLeft = canMoveLeft;
            movable.CanMoveUp = canMoveUp;
            movable.CanMoveRight = canMoveRight;
            movable.CanMoveDown = canMoveDown;
        }

        private bool IsTouchingLeft(Rectangle rectangle)
        {
            return movable.CollisionRectangle.Right + movable.Speed.X + 1 > rectangle.Left &&
                   movable.CollisionRectangle.Left < rectangle.Left &&
                   movable.CollisionRectangle.Bottom > rectangle.Top &&
                   movable.CollisionRectangle.Top < rectangle.Bottom;
        }

        private bool IsTouchingTop(Rectangle rectangle)
        {
            return movable.CollisionRectangle.Bottom + movable.Speed.Y + 10 > rectangle.Top &&
                   movable.CollisionRectangle.Top < rectangle.Top &&
                   movable.CollisionRectangle.Right > rectangle.Left &&
                   movable.CollisionRectangle.Left < rectangle.Right;
        }

        private bool IsTouchingRight(Rectangle rectangle)
        {
            return (movable.CollisionRectangle.Left - movable.Speed.X - 1 < rectangle.Right &&
                   movable.CollisionRectangle.Right > rectangle.Right &&
                   movable.CollisionRectangle.Bottom > rectangle.Top &&
                   movable.CollisionRectangle.Top < rectangle.Bottom) || 
                   movable.CollisionRectangle.Left < level.Bounds.Left;
        }

        private bool IsTouchingBottom(Rectangle rectangle)
        {
            return (movable.CollisionRectangle.Top + movable.Speed.Y < rectangle.Bottom &&
                   movable.CollisionRectangle.Bottom > rectangle.Bottom &&
                   movable.CollisionRectangle.Right > rectangle.Left &&
                   movable.CollisionRectangle.Left < rectangle.Right) || 
                   movable.CollisionRectangle.Top < level.Bounds.Top;
        }
    }
}