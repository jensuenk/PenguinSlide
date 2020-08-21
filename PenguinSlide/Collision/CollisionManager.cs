using Microsoft.Xna.Framework;
using PenguinSlide.Components;
using PenguinSlide.Entities;

namespace PenguinSlide.Collision
{
    public class CollisionManager
    {
        private readonly Level.Level level;
        private readonly Player player;

        public CollisionManager(Player player, Level.Level level)
        {
            this.player = player;
            this.level = level;
        }

        public void UpdateCollision()
        {
            UpdateMovement();
            UpdateDamage();
            UpdateCollectables();
            UpdatePortal();
        }

        private void UpdateMovement()
        {
            var canMoveLeft = true;
            var canMoveUp = true;
            var canMoveRight = true;
            var canMoveDown = true;

            foreach (var component in level.Tiles)
            {
                if (canMoveLeft)
                    canMoveLeft = !IsTouchingRight(component.CollisionRectangle);
                if (canMoveUp)
                    canMoveUp = !IsTouchingBottom(component.CollisionRectangle);
                if (canMoveRight)
                    canMoveRight = !IsTouchingLeft(component.CollisionRectangle);
                if (canMoveDown)
                    canMoveDown = !IsTouchingTop(component.CollisionRectangle);
            }

            player.CanMoveLeft = canMoveLeft;
            player.CanMoveUp = canMoveUp;
            player.CanMoveRight = canMoveRight;
            player.CanMoveDown = canMoveDown;
        }

        private void UpdatePortal()
        {
            if (player.CollisionRectangle.Intersects(level.Portal.CollisionRectangle))
                if (player.Collectables.Count == level.CollectablesAmount)
                    level.IsCompleted = true;
        }

        private void UpdateDamage()
        {
            if (!player.IsAlive) return;
            foreach (var component in level.Damageables)
                if (player.CollisionRectangle.Intersects(component.CollisionRectangle))
                {
                    player.IsAlive = false;
                    SoundPlayer.DieSound.Play();
                }
        }

        private void UpdateCollectables()
        {
            foreach (var component in level.ActiveCollectables)
                if (player.CollisionRectangle.Intersects(component.CollisionRectangle))
                {
                    player.Collectables.Add(component);
                    level.Components.Remove((Component) component);
                    SoundPlayer.PickupSound.Play();
                }
        }

        private bool IsTouchingLeft(Rectangle rectangle)
        {
            return player.CollisionRectangle.Right + player.Speed.X + 1 > rectangle.Left &&
                   player.CollisionRectangle.Left < rectangle.Left &&
                   player.CollisionRectangle.Bottom > rectangle.Top &&
                   player.CollisionRectangle.Top < rectangle.Bottom;
        }

        private bool IsTouchingTop(Rectangle rectangle)
        {
            return player.CollisionRectangle.Bottom + player.Speed.Y + 10 > rectangle.Top &&
                   player.CollisionRectangle.Top < rectangle.Top &&
                   player.CollisionRectangle.Right > rectangle.Left &&
                   player.CollisionRectangle.Left < rectangle.Right;
        }

        private bool IsTouchingRight(Rectangle rectangle)
        {
            return player.CollisionRectangle.Left - player.Speed.X - 1 < rectangle.Right &&
                   player.CollisionRectangle.Right > rectangle.Right &&
                   player.CollisionRectangle.Bottom > rectangle.Top &&
                   player.CollisionRectangle.Top < rectangle.Bottom ||
                   player.CollisionRectangle.Left < level.Bounds.Left;
        }

        private bool IsTouchingBottom(Rectangle rectangle)
        {
            return player.CollisionRectangle.Top + player.Speed.Y < rectangle.Bottom &&
                   player.CollisionRectangle.Bottom > rectangle.Bottom &&
                   player.CollisionRectangle.Right > rectangle.Left &&
                   player.CollisionRectangle.Left < rectangle.Right ||
                   player.CollisionRectangle.Top < level.Bounds.Top;
        }
    }
}