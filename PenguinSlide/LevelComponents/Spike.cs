using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.LevelComponents
{
    public class Spike : Component, IDamageable
    {
        public Spike(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
        }
    }
}