using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

 namespace PenguinSlide
{
    public class Level
    {
        private ContentManager contentManager;
        private int[,] map;
        private List<Tile> tiles = new List<Tile>();
        public List<Tile> Tiles
        {
            get { return tiles; }
        }

        private int width, height;

        public Level(ContentManager contentManager, int[,] map)
        {
            this.contentManager = contentManager;
            this.map = map;
            Generate();
        }
        private void Generate()
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number == 1)
                    {
                        tiles.Add(new Tile(contentManager.Load<Texture2D>("tiles/block_ground_00_single"), new Vector2(x*128, y*128)));
                    }
                    width = (x + 1) * map.GetLength(1);
                    height = (y + 1) * map.GetLength(0);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
