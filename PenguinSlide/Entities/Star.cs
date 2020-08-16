using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide
{
    class Star
    {
        private Vector2 position;
        private Texture2D texture;
        public Rectangle CollisionRectangle;

        public Star(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
