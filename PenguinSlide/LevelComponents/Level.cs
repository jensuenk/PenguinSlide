using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PenguinSlide.Entities;

namespace PenguinSlide.LevelComponents
{
    public class Level
    {
        private ContentManager contentManager;
        private int[,] map;
        private int width, height, size;
        private int windowWidth = 1920;
        private int windowHeight= 1080;
        public Vector2 PlayerLocation { get; private set; }
        public int PlayerSize { get; private set; }
        public List<Tile> Tiles { get; } = new List<Tile>();
        public Level(ContentManager contentManager, int[,] map)
        {
            this.contentManager = contentManager;
            this.map = map;
            width = windowWidth / map.GetLength(1);
            height = windowHeight / map.GetLength(0);
            size = height;
            PlayerSize = size;
            Generate();
        }
        private void Generate()
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    switch (number)
                    {
                        case 1:
                            Tiles.Add(new Tile(contentManager.Load<Texture2D>("tiles/block_ground_00_single"), new Rectangle(x * size, y * size, size, size)));
                            break;
                        case 2:
                            PlayerLocation = new Vector2(x * size, y * size);
                            break;
                    }
                    
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
