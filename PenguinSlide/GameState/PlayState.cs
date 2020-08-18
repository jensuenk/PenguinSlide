﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide
{
    class PlayState : State
    {
        private LevelManager levelManager;
        private Level currentLevel;
        private Player player;
        private Control control;
        private CollisionManager collisionManager;

        public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(graphicsDevice, contentManager, game)
        {
            levelManager = new LevelManager(contentManager);
            levelManager.GenerateLevels();
            currentLevel = levelManager.CurrentLevel;
            
            Texture2D playerTexture = contentManager.Load<Texture2D>("player");

            control = new KeyboardControl();
            Vector2 playerPosition = new Vector2(0, 550);
            Vector2 playerSpeed = new Vector2(7, 1);

            Rectangle playerCollisonRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 144, playerTexture.Height);
            player = new Player(playerTexture, playerCollisonRectangle, playerPosition, playerSpeed, 0.5F, control);
            
            collisionManager = new CollisionManager();
        }

        public override void Update(GameTime gameTime)
        {
            control.Update();
            //collisionManager.UpdateCollision(player, currentLevel);
            player.Update(gameTime);
            collisionManager.CheckCollision(player, currentLevel);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            currentLevel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
