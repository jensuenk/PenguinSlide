using Microsoft.Xna.Framework;

namespace PenguinSlide.Collision
{
    public interface ICollidable
    {
        Rectangle CollisionRectangle { get; set; }
    }
}
