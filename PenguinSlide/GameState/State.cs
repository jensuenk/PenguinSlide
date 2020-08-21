using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.GameState
{
    public abstract class State
    {
        protected readonly ContentManager ContentManager;
        protected readonly PenguinSlide Game;
        protected readonly GraphicsDevice GraphicsDevice;

        protected State(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game)
        {
            GraphicsDevice = graphicsDevice;
            ContentManager = contentManager;
            Game = game;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}