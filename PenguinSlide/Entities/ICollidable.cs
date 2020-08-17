using Microsoft.Xna.Framework;

namespace PenguinSlide
{
    public interface ICollidable
    {
        Rectangle CollisionRectangle { get; set; }
    }
}
