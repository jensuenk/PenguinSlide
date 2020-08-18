using Microsoft.Xna.Framework;

namespace PenguinSlide.Entities
{
    public interface ICollidable
    {
        Rectangle CollisionRectangle { get; set; }
    }
}
