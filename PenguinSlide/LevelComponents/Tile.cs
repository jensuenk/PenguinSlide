using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;

namespace PenguinSlide.LevelComponents
{
    public class Tile : Component
    {
        public Tile(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
        }
    }
}