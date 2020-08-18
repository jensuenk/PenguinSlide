using Microsoft.Xna.Framework;

namespace PenguinSlide
{
    public interface IMovable : ICollidable
    {
        Vector2 Speed { get; }
        bool CanMoveLeft { get; set; }
        bool CanMoveRight { get; set; }
        bool CanMoveUp { get; set; }
        bool CanMoveDown { get; set; }
    }
}
