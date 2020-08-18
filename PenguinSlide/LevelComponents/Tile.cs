using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;

namespace PenguinSlide.LevelComponents
{
    public class Tile: ICollidable
    {
        private Texture2D texture;
        public Rectangle CollisionRectangle { get; set; }

        public Tile(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            CollisionRectangle = rectangle;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionRectangle, Color.White);
        }
    }
}
