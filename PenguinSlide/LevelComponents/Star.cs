using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;

namespace PenguinSlide.LevelComponents
{
    public class Star : ICollidable
    {
        private readonly Vector2 position;
        private readonly Texture2D texture;

        public Star(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            CollisionRectangle = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            this.position = position;
        }

        public Rectangle CollisionRectangle { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}