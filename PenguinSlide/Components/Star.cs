using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;

namespace PenguinSlide.Components
{
    public class Star : Component, ICollidable, ICollectable
    {
        public Star(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            CollisionRectangle = rectangle;
        }

        public Rectangle CollisionRectangle { get; set; }
    }
}