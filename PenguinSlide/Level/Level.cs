﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PenguinSlide
{
    class Level
    {
        private List<Tile> tiles = new List<Tile>();
        public List<Tile> Tiles
        {
            get { return tiles; }
        }
        private List<Star> stars = new List<Star>();
        public List<Star> Stars
        {
            get { return stars; }
        }

        private int width, height;
        public int Width {
            get { return width; }
        }
        public int Height {
            get { return height; }
        }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number > 0 && number != 10)
                    {
                        //tiles.Add(new Tile(Resources.LoadFile["Tile"], new Rectangle(x * size, y * size, size, size)));
                    }
                    if (number == 10)
                    {
                        //stars.Add(new Star(new Vector2(x * size, y * size)));
                    }
                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
