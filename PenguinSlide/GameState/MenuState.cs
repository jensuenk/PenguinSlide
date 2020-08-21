using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PenguinSlide.Components;
using PenguinSlide.Level;

namespace PenguinSlide.GameState
{
    internal class MenuState : State
    {
        private readonly Background background;
        private readonly List<Button> buttons = new List<Button>();

        public MenuState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(
            graphicsDevice, contentManager, game)
        {
            MediaPlayer.Resume();
            game.IsMouseVisible = true;

            var playButtonTexture = contentManager.Load<Texture2D>("ui/play-button");
            var quitButtonTexture = contentManager.Load<Texture2D>("ui/quit-button");
            var backgroundTexture = contentManager.Load<Texture2D>("ui/start-screen");

            background = new Background(backgroundTexture,
                new Rectangle(0, 0, backgroundTexture.Width, graphicsDevice.Viewport.Bounds.Height));

            var playButton = new Button(playButtonTexture,
                new Rectangle(110, 300, playButtonTexture.Width, playButtonTexture.Height));
            playButton.Click += PlayButton_Click;
            var quitButton = new Button(quitButtonTexture,
                new Rectangle(110, 600, quitButtonTexture.Width, quitButtonTexture.Height));
            quitButton.Click += QuitButton_Click;
            buttons.Add(playButton);
            buttons.Add(quitButton);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in buttons)
                button.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (var button in buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MediaPlayer.Pause();
            SoundPlayer.ButtonSound.Play();
            game.ChangeState(new PlayState(graphicsDevice, contentManager, game));
            game.IsMouseVisible = false;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}