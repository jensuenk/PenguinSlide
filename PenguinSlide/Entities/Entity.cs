using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.Entities
{
    public abstract class Entity
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
