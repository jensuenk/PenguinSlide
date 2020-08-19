using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Collision;
using PenguinSlide.Controls;
using PenguinSlide.Entities;
using PenguinSlide.Level;

namespace PenguinSlide.GameState
{
    internal class PlayState : State
    {
        private readonly Background background;
        private readonly Camera camera;
        private readonly CollisionManager collisionManager;
        private readonly Control control;
        private readonly Level.Level currentLevel;
        private readonly LevelManager levelManager;
        private readonly Player player;

        public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(
            graphicsDevice, contentManager, game)
        {
            game.IsMouseVisible = false;
            
            levelManager = new LevelManager(contentManager, graphicsDevice.Viewport);
            levelManager.GenerateLevels();
            currentLevel = levelManager.CurrentLevel;

            var playerTexture = contentManager.Load<Texture2D>("player");
            var backgroundTexture = contentManager.Load<Texture2D>("bg-icebergs-1");

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

            camera = new Camera(graphicsDevice.Viewport);
        }

        public override void Update(GameTime gameTime)
        {
            if (player.PlayedDieAnimation)
            {
                player.Respawn(currentLevel.PlayerLocation);
            }
            control.Update();
            collisionManager.UpdateCollision();
            player.Update(gameTime);
            camera.Follow(player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            background.Draw(spriteBatch);
            currentLevel.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}