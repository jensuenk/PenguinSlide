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
        private CollisionManager collisionManager;
        private readonly Control control;
        private Level.Level currentLevel;
        private readonly LevelManager levelManager;
        private readonly Player player;
        private List<Button> buttons = new List<Button>();
        private Background respawnScreen;

        public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(
            graphicsDevice, contentManager, game)
        {
            levelManager = new LevelManager(contentManager, graphicsDevice.Viewport);
            levelManager.GenerateLevels();
            currentLevel = levelManager.CurrentLevel;

            var playerTexture = contentManager.Load<Texture2D>("player");
            var backgroundTexture = contentManager.Load<Texture2D>("bg-icebergs-1");
            var respawnBackgroundTexture = contentManager.Load<Texture2D>("respawn-screen");
            Texture2D respawnButtonTexture = contentManager.Load<Texture2D>("respawn-button-small");
            Texture2D quitButtonTexture = contentManager.Load<Texture2D>("quit-button-small");

            control = new KeyboardControl();
            var playerPosition = currentLevel.PlayerLocation;
            var playerSpeed = new Vector2(7, 10);
            var playerScale = (float) currentLevel.PlayerSize / 144;

            var playerCollisionRectangle = 
                new Rectangle((int) playerPosition.X, (int) playerPosition.Y, 144, playerTexture.Height);
            player = new Player(playerTexture, playerCollisionRectangle, playerSpeed, playerScale, control);

            collisionManager = new CollisionManager(player, currentLevel);
            background = new Background(backgroundTexture,
                new Rectangle(0, 0, backgroundTexture.Width, graphicsDevice.Viewport.Bounds.Height));
            
            respawnScreen = new Background(respawnBackgroundTexture,
                new Rectangle(0, 0, graphicsDevice.Viewport.Bounds.Width, graphicsDevice.Viewport.Bounds.Height));
            var respawnButton = new Button(respawnButtonTexture,
                new Rectangle(1025, 700, 160, 160));
            respawnButton.Click += RespawnButtonClick;

            var quitButton = new Button(quitButtonTexture,
                new Rectangle(725, 700, 160, 160));

            quitButton.Click += QuitButtonClick;
            buttons.Add(respawnButton);
            buttons.Add(quitButton);

            camera = new Camera(graphicsDevice.Viewport);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!player.IsAlive)
            {
                player.Position = currentLevel.PlayerLocation;
                foreach (var button in buttons)
                    button.Update(gameTime);
            }

            control.Update();
            player.Update(gameTime);
            collisionManager.UpdateCollision();
            camera.Follow(player);
            if (currentLevel.IsCompleted)
            {
                if (levelManager.IsLastLevel(currentLevel))
                {
                    game.ChangeState(new EndState(graphicsDevice, contentManager, game));
                }
                else
                {
                    currentLevel = levelManager.GetNextLevel();
                    levelManager.CurrentLevel = currentLevel;
                    player.Position = currentLevel.PlayerLocation;
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
            if (!player.IsAlive)
            {
                game.IsMouseVisible = true;
                respawnScreen.Draw(spriteBatch);
                foreach (var button in buttons)
                    button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        private void RespawnButtonClick(object sender, System.EventArgs e)
        {
            player.IsAlive = true;
            player.Respawn(currentLevel.PlayerLocation);
            game.IsMouseVisible = false;
        }
        private void QuitButtonClick(object sender, System.EventArgs e)
        {
            game.ChangeState(new MenuState(graphicsDevice, contentManager, game));
        }
    }
}