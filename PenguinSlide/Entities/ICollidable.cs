using Microsoft.Xna.Framework;

namespace PenguinSlide
{
    interface ICollidable
    {
        Rectangle CollisionRectangle { get; set; }
    }
}
