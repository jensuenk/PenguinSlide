using Microsoft.Xna.Framework;
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
        private Camera camera;

        public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, PenguinSlide game) : base(graphicsDevice, contentManager, game)
        {
            levelManager = new LevelManager(contentManager);
            levelManager.GenerateLevels();
            currentLevel = levelManager.CurrentLevel;
            
            Texture2D playerTexture = contentManager.Load<Texture2D>("player");
            Texture2D backgroundTexture = contentManager.Load<Texture2D>("bg-icebergs-1");

            control = new KeyboardControl();
            Vector2 playerPosition = currentLevel.PlayerLocation;
            Vector2 playerSpeed = new Vector2(7, 10);
            float playerScale = (float)currentLevel.PlayerSize / 144;

            Rectangle playerCollisionRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 144, playerTexture.Height);
            player = new Player(playerTexture, playerCollisionRectangle, playerSpeed, playerScale, control);
            
            collisionManager = new CollisionManager(player, currentLevel);
            background = new Background(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, graphicsDevice.Viewport.Bounds.Height));
            
            camera = new Camera(graphicsDevice.Viewport);
        }
        public override void Update(GameTime gameTime)
        {
            control.Update();
            collisionManager.UpdateCollision();
            player.Update(gameTime);
            camera.Follow(player);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            currentLevel.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
