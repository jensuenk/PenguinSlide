using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PenguinSlide
{
    public class CollisionManager
    {
        
        public void CheckCollision(Player player, Level currentLevel)
        {
            CheckPlatformCollision(player, currentLevel);
        }



        private void CheckPlatformCollision(Player player, Level level)
        {
            bool canMoveDown = true;
            bool canMoveLeft = true;
            bool canMoveRight = true;
            bool canMoveUp = true;

            foreach (Tile block in level.Tiles)
            {
                    if (canMoveDown == true)
                    {
                        canMoveDown = !(RectangleHelper.CheckTopCollision(player, block));
                    }

                    if (canMoveRight == true)
                    {
                        canMoveRight = !(RectangleHelper.CheckLeftCollision(player, block));
                    }

                    if (canMoveLeft == true)
                    {
                        canMoveLeft = !(RectangleHelper.CheckRightCollision(player, block));
                    }

                    if (canMoveUp)
                    {
                        canMoveUp = !(RectangleHelper.CheckBottomCollision(player, block));
                    }

                
            }

            player.CanMoveDown = canMoveDown;
            player.CanMoveLeft = canMoveLeft;
            player.CanMoveRight = canMoveRight;
            player.CanMoveUp = canMoveUp;

        }

    }
}