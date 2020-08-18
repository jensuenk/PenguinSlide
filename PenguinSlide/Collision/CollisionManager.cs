using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PenguinSlide
{
    public class CollisionManager
    {
        public void UpdateCollision(Player player, Level level)
        {
            var canMoveDown = true;
            var canMoveLeft = true;
            var canMoveRight = true;
            var canMoveUp = true;

            foreach (var tile in level.Tiles)
            {
                if (canMoveDown)
                    canMoveDown = !(RectangleHelper.CheckTopCollision(player, tile.CollisionRectangle));
                if (canMoveRight)
                    canMoveRight = !(RectangleHelper.CheckLeftCollision(player, tile.CollisionRectangle));
                if (canMoveLeft)
                    canMoveLeft = !(RectangleHelper.CheckRightCollision(player, tile.CollisionRectangle));
                if (canMoveUp)
                    canMoveUp = !(RectangleHelper.CheckBottomCollision(player, tile.CollisionRectangle));
            }
            
            player.CanMoveDown = canMoveDown;
            player.CanMoveLeft = canMoveLeft;
            player.CanMoveRight = canMoveRight;
            player.CanMoveUp = canMoveUp;
        }
    }
}