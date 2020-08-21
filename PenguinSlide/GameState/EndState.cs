using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;
using PenguinSlide.Level;
using PenguinSlide.Sound;

namespace PenguinSlide.GameState
{
    internal class EndState : State
    {
        private readonly List<Button> buttons = new List<Button>();
        private readonly Background endScreen;

        public EndState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(
            graphicsDevice, contentManager, game)
        {
            game.IsMouseVisible = true;
            var respawnBackgroundTexture = contentManager.Load<Texture2D>("ui/gameover-screen");
            var respawnButtonTexture = contentManager.Load<Texture2D>("ui/respawn-button-small");
            var quitButtonTexture = contentManager.Load<Texture2D>("ui/quit-button-small");

            endScreen = new Background(respawnBackgroundTexture,
                new Rectangle(0, 0, graphicsDevice.Viewport.Bounds.Width, graphicsDevice.Viewport.Bounds.Height));
            
            var againButton = new Button(respawnButtonTexture,
                new Rectangle(1025, 700, 160, 160));
            againButton.Click += RespawnButton_Click;
            var quitButton = new Button(quitButtonTexture,
                new Rectangle(725, 700, 160, 160));
            quitButton.Click += QuitButton_Click;
            buttons.Add(againButton);
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
            endScreen.Draw(spriteBatch);
            foreach (var button in buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void RespawnButton_Click(object sender, EventArgs e)
        {
            SoundPlayer.ButtonSound.Play();
            Game.ChangeState(new PlayState(GraphicsDevice, ContentManager, Game));
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            SoundPlayer.ButtonSound.Play();
            Game.ChangeState(new MenuState(GraphicsDevice, ContentManager, Game));
        }
    }
}