using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PenguinSlide
{
    public class CollisionManager
    {
        public void UpdateCollision(Player player, Level level)
        {
            player.CanMoveLeft = true;
            player.CanMoveRight = true;
            player.CanMoveUp = true;
            player.CanMoveDown = true;
            
            Rectangle playerRectangle = player.CollisionRectangle;
            foreach (var tile in level.Tiles)
            {
                if (playerRectangle.Intersects(tile.CollisionRectangle))
                    player.CanMoveDown = false;
                if (IsTouchingLeft(playerRectangle, tile.CollisionRectangle))
                {
                    player.CanMoveLeft = false;
                }
                if (IsTouchingRight(playerRectangle, tile.CollisionRectangle))
                {
                    player.CanMoveRight = false;
                }
                if (IsTouchingTop(playerRectangle, tile.CollisionRectangle))
                {
                    player.CanMoveUp = false;
                }
                if (IsTouchingBottom(playerRectangle, tile.CollisionRectangle))
                {
                    player.CanMoveDown = false;
                }
            }
        }
        public bool canMove(ICollidable collidable, Level level)
        {
            foreach (var tile in level.Tiles)
            {
                if (collidable.CollisionRectangle.Intersects(tile.CollisionRectangle))
                {
                    return false;
                }
            }

            return true;
        } 
        private bool IsTouchingLeft(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Right > rectangle2.Left &&
                   rectangle1.Left < rectangle2.Left &&
                   rectangle1.Bottom > rectangle2.Top &&
                   rectangle1.Top < rectangle2.Bottom;
        }

        private bool IsTouchingRight(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Left < rectangle2.Right &&
                   rectangle1.Right > rectangle2.Right &&
                   rectangle1.Bottom > rectangle2.Top &&
                   rectangle1.Top < rectangle2.Bottom;
        }

        private bool IsTouchingTop(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Bottom > rectangle2.Top &&
                   rectangle1.Top < rectangle2.Top &&
                   rectangle1.Right > rectangle2.Left &&
                   rectangle1.Left < rectangle2.Right;
        }

        private bool IsTouchingBottom(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Top < rectangle2.Bottom &&
                   rectangle1.Bottom > rectangle2.Bottom &&
                   rectangle1.Right > rectangle2.Left &&
                   rectangle1.Left < rectangle2.Right;
        }
    }
}