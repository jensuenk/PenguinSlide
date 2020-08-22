using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.Components
{
    public class Score
    {
        private readonly SpriteFont font;
        private readonly Vector2 position;
        public int Collected;
        public int Needed;

        public Score(SpriteFont font, Vector2 position)
        {
            this.font = font;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Collected + "/" + Needed, position, Color.White);
        }
    }
}