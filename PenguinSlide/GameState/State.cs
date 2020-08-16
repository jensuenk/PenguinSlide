using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide
{
    public abstract class State
    {
        protected ContentManager ContentManager;
        protected GraphicsDevice GraphicsDevice;
        protected PenguinSlide Game;
        public State(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game)
        {
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;
            this.Game = game;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
