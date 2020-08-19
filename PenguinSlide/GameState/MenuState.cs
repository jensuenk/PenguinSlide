using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;
using PenguinSlide.Interface;
using PenguinSlide.Level;

namespace PenguinSlide.GameState
{
    internal class MenuState : State
    {
        private List<Button> buttons = new List<Button>();
        private Component logo;
        private Background background;
        public MenuState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(graphicsDevice, contentManager, game)
        {
            game.IsMouseVisible = true;
            
            Texture2D logoTexture = contentManager.Load<Texture2D>("logo");
            Texture2D playButtonTexture = contentManager.Load<Texture2D>("play-button");
            Texture2D quitButtonTexture = contentManager.Load<Texture2D>("quit-button");
            Texture2D backgroundTexture = contentManager.Load<Texture2D>("bg-icebergs-1");
            
            background = new Background(backgroundTexture,
                new Rectangle(0, 0, backgroundTexture.Width, graphicsDevice.Viewport.Bounds.Height));

            int logoWidth = (int)(logoTexture.Width * 0.4);
            int logoHeight = (int)(logoTexture.Height * 0.4);
            logo = new Component(logoTexture, new Rectangle(graphicsDevice.Viewport.Width / 2 - logoWidth / 2, graphicsDevice.Viewport.Height / 9, logoWidth, logoHeight));
            
            int buttonWidth = (int)(playButtonTexture.Width * 0.8);
            int buttonHeight = (int)(playButtonTexture.Height * 0.8);
            
            var playButton = new Button(playButtonTexture,
                new Rectangle(graphicsDevice.Viewport.Width / 2 - buttonWidth / 2, (int)(graphicsDevice.Viewport.Height * 0.6), buttonWidth, buttonHeight));
            playButton.Click += PlayButtonClick;

            var quitButton = new Button(quitButtonTexture,
                new Rectangle(graphicsDevice.Viewport.Width / 2 - buttonWidth / 2, (int)(graphicsDevice.Viewport.Height * 0.6) + buttonHeight + 20, buttonWidth, buttonHeight));

            quitButton.Click += QuitButtonClick;
            
            buttons.Add(playButton);
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
            background.Draw(spriteBatch);
            logo.Draw(spriteBatch);
            foreach (var button in buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }
        
        private void PlayButtonClick(object sender, System.EventArgs e)
        {
            game.ChangeState(new PlayState(graphicsDevice, contentManager, game));
        }
        private void QuitButtonClick(object sender, System.EventArgs e)
        {
            game.Exit();
        }

    }
}