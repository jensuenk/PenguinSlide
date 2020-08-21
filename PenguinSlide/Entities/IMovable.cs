using Microsoft.Xna.Framework;

namespace PenguinSlide.Entities
{
    public interface IMovable
    {
        Vector2 Speed { get; }
        Vector2 Position { get; set; }
        bool CanMoveLeft { get; set; }
        bool CanMoveRight { get; set; }
        bool CanMoveUp { get; set; }
        bool CanMoveDown { get; set; }
    }
}