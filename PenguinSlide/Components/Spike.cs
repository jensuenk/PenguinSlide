using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.Components
{
    public class Spike : Component, IDamageable
    {
        public Spike(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            CollisionRectangle = rectangle;
        }

        public Rectangle CollisionRectangle { get; set; }
    }
}