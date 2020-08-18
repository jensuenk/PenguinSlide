using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;

namespace PenguinSlide.LevelComponents
{
    public class Tile: ICollidable
    {
        private Texture2D texture;
        public Vector2 Position;
        public Rectangle CollisionRectangle { get; set; }

        public Tile(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.Position = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionRectangle, Color.White);
        }
    }
}
