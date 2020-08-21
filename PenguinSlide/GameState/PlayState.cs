using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;
using PenguinSlide.Components;
using PenguinSlide.Controls;
using PenguinSlide.Entities;
using PenguinSlide.Level;

namespace PenguinSlide.GameState
{
    internal class PlayState : State
    {
        private readonly Background background;
        private readonly Camera camera;
        private readonly Control control;
        private readonly LevelManager levelManager;
        private readonly Player player;
        private readonly List<Button> buttons = new List<Button>();
        private CollisionManager collisionManager;
        private Level.Level currentLevel;
        private readonly Background respawnBackground;

        public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(
            graphicsDevice, contentManager, game)
        {
            levelManager = new LevelManager(contentManager, graphicsDevice.Viewport);
            levelManager.GenerateLevels();
            currentLevel = levelManager.CurrentLevel;
            
            var playerTexture = contentManager.Load<Texture2D>("player/player-sprite");
            var backgroundTexture = contentManager.Load<Texture2D>("level/game-background");
            var respawnBackgroundTexture = contentManager.Load<Texture2D>("ui/respawn-screen");
            var respawnButtonTexture = contentManager.Load<Texture2D>("ui/respawn-button-small");
            var quitButtonTexture = contentManager.Load<Texture2D>("ui/quit-button-small");

            control = new KeyboardControl();
            
            camera = new Camera(graphicsDevice.Viewport);
            
            var playerPosition = currentLevel.PlayerLocation;
            var playerSpeed = new Vector2(7, 10);
            var playerCollisionRectangle =
                new Rectangle((int) playerPosition.X, (int) playerPosition.Y, 144, playerTexture.Height);
            player = new Player(playerTexture, playerCollisionRectangle, playerSpeed, (float) currentLevel.TileSize / 144, control);

            collisionManager = new CollisionManager(player, currentLevel);
            
            background = new Background(backgroundTexture,
                new Rectangle(0, 0, backgroundTexture.Width, graphicsDevice.Viewport.Bounds.Height));
            respawnBackground = new Background(respawnBackgroundTexture,
                new Rectangle(0, 0, graphicsDevice.Viewport.Bounds.Width, graphicsDevice.Viewport.Bounds.Height));
            
            var respawnButton = new Button(respawnButtonTexture,
                new Rectangle(1025, 700, 160, 160));
            respawnButton.Click += RespawnButton_Click;
            var quitButton = new Button(quitButtonTexture,
                new Rectangle(725, 700, 160, 160));
            quitButton.Click += QuitButton_Click;
            buttons.Add(respawnButton);
            buttons.Add(quitButton);
        }

        public override void Update(GameTime gameTime)
        {
            if (!player.IsAlive)
            {
                player.Position = currentLevel.PlayerLocation;
                foreach (var button in buttons)
                    button.Update();
            }

            control.Update();
            player.Update(gameTime);
            foreach (var enemy in currentLevel.Enemies)
            {
                enemy.Update(gameTime);
            }
            collisionManager.UpdateCollision();
            camera.Follow(player);
            
            if (currentLevel.IsCompleted)
            {
                SoundPlayer.EndSound.Play();
                if (levelManager.IsLastLevel(currentLevel))
                {
                    game.ChangeState(new EndState(graphicsDevice, contentManager, game));
                }
                else
                {
                    currentLevel = levelManager.GetNextLevel();
                    levelManager.CurrentLevel = currentLevel;
                    player.Respawn(currentLevel.PlayerLocation);
                    collisionManager = new CollisionManager(player, currentLevel);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            
            background.Draw(spriteBatch);
            currentLevel.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (var enemy in currentLevel.Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            
            if (!player.IsAlive)
            {
                game.IsMouseVisible = true;
                respawnBackground.Draw(spriteBatch);
                foreach (var button in buttons)
                    button.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        private void RespawnButton_Click(object sender, EventArgs e)
        {
            SoundPlayer.ButtonSound.Play();
            player.IsAlive = true;
            player.Respawn(currentLevel.PlayerLocation);
            currentLevel.RespawnCollectables();
            game.IsMouseVisible = false;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            SoundPlayer.ButtonSound.Play();
            game.ChangeState(new MenuState(graphicsDevice, contentManager, game));
        }
    }
}