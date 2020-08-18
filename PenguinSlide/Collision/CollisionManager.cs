using System;
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
                if (IsTouchingLeft(playerRectangle, tile.CollisionRectangle, player))
                {
                    player.CanMoveLeft = false;
                }
                if (IsTouchingRight(playerRectangle, tile.CollisionRectangle, player))
                {
                    player.CanMoveRight = false;
                }
                if (IsTouchingTop(playerRectangle, tile.CollisionRectangle, player))
                {
                    player.CanMoveUp = false;
                }
                if (IsTouchingBottom(playerRectangle, tile.CollisionRectangle, player))
                {
                    player.CanMoveDown = false;
                }
            }
        }
        public bool CanMove(ICollidable collidable, Level level)
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
        private bool IsTouchingLeft(Rectangle rectangle1, Rectangle rectangle2, Player player)
        {
            return rectangle1.Right + 1 > rectangle2.Left &&
                   rectangle1.Left < rectangle2.Left &&
                   rectangle1.Bottom > rectangle2.Top &&
                   rectangle1.Top < rectangle2.Bottom;
        }

        private bool IsTouchingRight(Rectangle rectangle1, Rectangle rectangle2, Player player)
        {
            return rectangle1.Left - 1 < rectangle2.Right &&
                   rectangle1.Right > rectangle2.Right &&
                   rectangle1.Bottom > rectangle2.Top &&
                   rectangle1.Top < rectangle2.Bottom;
        }

        private bool IsTouchingTop(Rectangle rectangle1, Rectangle rectangle2, Player player)
        {
            return rectangle1.Bottom + 1 > rectangle2.Top &&
                   rectangle1.Top < rectangle2.Top &&
                   rectangle1.Right > rectangle2.Left &&
                   rectangle1.Left < rectangle2.Right;
        }

        private bool IsTouchingBottom(Rectangle rectangle1, Rectangle rectangle2, Player player)
        {
            return rectangle1.Top - 1 < rectangle2.Bottom &&
                   rectangle1.Bottom > rectangle2.Bottom &&
                   rectangle1.Right > rectangle2.Left &&
                   rectangle1.Left < rectangle2.Right;
        }
    }
}