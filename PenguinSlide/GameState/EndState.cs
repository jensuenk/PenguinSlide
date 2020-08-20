using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;
using PenguinSlide.Level;

namespace PenguinSlide.GameState
{
    internal class EndState : State
    {
        private List<Button> buttons = new List<Button>();
        private Background endScreen;
        public EndState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(graphicsDevice, contentManager, game)
        {
            game.IsMouseVisible = true;
            var respawnBackgroundTexture = contentManager.Load<Texture2D>("gameover-screen");
            Texture2D respawnButtonTexture = contentManager.Load<Texture2D>("respawn-button-small");
            Texture2D quitButtonTexture = contentManager.Load<Texture2D>("quit-button-small");
            
            endScreen = new Background(respawnBackgroundTexture,
                new Rectangle(0, 0, graphicsDevice.Viewport.Bounds.Width, graphicsDevice.Viewport.Bounds.Height));
            var againButton = new Button(respawnButtonTexture,
                new Rectangle(1025, 700, 160, 160));
            againButton.Click += RespawnButtonClick;

            var quitButton = new Button(quitButtonTexture,
                new Rectangle(725, 700, 160, 160));

            quitButton.Click += QuitButtonClick;
            buttons.Add(againButton);
            buttons.Add(quitButton);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in buttons)
                button.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            endScreen.Draw(spriteBatch);
            foreach (var button in buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }
        private void RespawnButtonClick(object sender, System.EventArgs e)
        {
            game.ChangeState(new PlayState(graphicsDevice, contentManager, game));
        }
        private void QuitButtonClick(object sender, System.EventArgs e)
        {
            game.ChangeState(new MenuState(graphicsDevice, contentManager, game));
        }
    }
}