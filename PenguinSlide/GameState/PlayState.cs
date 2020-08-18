﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;
using PenguinSlide.Controls;
using PenguinSlide.Entities;
using PenguinSlide.LevelComponents;

namespace PenguinSlide.GameState
{
    class PlayState : State
    {
        private LevelManager levelManager;
        private Level currentLevel;
        private Player player;
        private Control control;
        private CollisionManager collisionManager;
        private Background background;

        public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(graphicsDevice, contentManager, game)
        {
            levelManager = new LevelManager(contentManager);
            levelManager.GenerateLevels();
            currentLevel = levelManager.CurrentLevel;
            
            Texture2D playerTexture = contentManager.Load<Texture2D>("player");
            Texture2D backgroundtexture = contentManager.Load<Texture2D>("bg-icebergs-1");

            control = new KeyboardControl();
            Vector2 playerPosition = new Vector2(0, 550);
            Vector2 playerSpeed = new Vector2(7, 10);

            Rectangle playerCollisonRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 144, playerTexture.Height);
            player = new Player(playerTexture, playerCollisonRectangle, playerPosition, playerSpeed, 0.5F, control);
            
            collisionManager = new CollisionManager(player, currentLevel);
            background = new Background(backgroundtexture, graphicsDevice.Viewport.Bounds);
        }
        public override void Update(GameTime gameTime)
        {
            control.Update();
            collisionManager.UpdateCollision();
            player.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            currentLevel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
