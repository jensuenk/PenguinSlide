using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;

namespace PenguinSlide.Level
{
    public class Background : Component
    {
        public Background(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
        }
    }
}