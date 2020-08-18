using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.GameState
{
    public abstract class State
    {
        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;
        protected PenguinSlide game;
        public State(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.game = game;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
