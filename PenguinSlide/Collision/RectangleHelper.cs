using Microsoft.Xna.Framework;

namespace PenguinSlide
{
    public static class RectangleHelper
    {
        public static bool CheckTopCollision(Player player, Rectangle rectangle)
        {
            return (player.CollisionRectangle.Bottom + player.Speed.Y + 10 > rectangle.Top &&
                    player.CollisionRectangle.Top < rectangle.Top &&
                    player.CollisionRectangle.Right > rectangle.Left &&
                    player.CollisionRectangle.Left < rectangle.Right);
        }

        public static bool CheckBottomCollision(Player player, Rectangle rectangle)
        {
            return (player.CollisionRectangle.Top + player.Speed.Y - 1 < rectangle.Bottom &&
                    player.CollisionRectangle.Bottom > rectangle.Bottom &&
                    player.CollisionRectangle.Right > rectangle.Left &&
                    player.CollisionRectangle.Left < rectangle.Right);
        }

        public static bool CheckRightCollision(Player player, Rectangle rectangle)
        {
            return (player.CollisionRectangle.Left - player.Speed.X + 1 < rectangle.Right &&
                    player.CollisionRectangle.Right > rectangle.Right &&
                    player.CollisionRectangle.Bottom > rectangle.Top &&
                    player.CollisionRectangle.Top < rectangle.Bottom);
        }

        public static bool CheckLeftCollision(Player player, Rectangle rectangle)
        {
            return (player.CollisionRectangle.Right + player.Speed.X + 1 > rectangle.Left &&
                    player.CollisionRectangle.Left < rectangle.Left &&
                    player.CollisionRectangle.Bottom > rectangle.Top &&
                    player.CollisionRectangle.Top < rectangle.Bottom);
        }
    }
}