using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.Components
{
    public abstract class Component
    {
        protected Rectangle Rectangle;
        protected readonly Texture2D Texture;

        protected Component(Texture2D texture, Rectangle rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}