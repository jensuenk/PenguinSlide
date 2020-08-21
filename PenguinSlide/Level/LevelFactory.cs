using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;
using PenguinSlide.Entities;

namespace PenguinSlide.Level
{
    public class LevelFactory
    {
        public Component CreateComponent(int id, Vector2 position, int tileSize, ContentManager contentManager)
        {
            var x = (int)position.X * tileSize;
            var y = (int)position.Y * tileSize;
            
            switch (id)
            {
                case 2:
                    return new Tile(contentManager.Load<Texture2D>("level/ice-tile"),
                        new Rectangle(x, y, tileSize, tileSize));
                case 3:
                    return new Star(contentManager.Load<Texture2D>("level/star"),
                        new Rectangle(x, y, tileSize, tileSize));
                case 4:
                    return new Spike(contentManager.Load<Texture2D>("level/spikes"),
                        new Rectangle(x, y + tileSize / 2, tileSize, tileSize / 2));
                case 5:
                    return new Decoration(contentManager.Load<Texture2D>("level/tree"),
                        new Rectangle(x, y, tileSize * 2, tileSize * 2));
                case 6:
                    return new Tile(contentManager.Load<Texture2D>("level/ice-tile-clear"),
                        new Rectangle(x, y, tileSize, tileSize));
                case 7:
                    return new Portal(contentManager.Load<Texture2D>("level/igloo"),
                        new Rectangle(x, y, tileSize * 2, tileSize));
                case 8:
                    return new Tile(contentManager.Load<Texture2D>("level/crate"),
                        new Rectangle(x, y, tileSize, tileSize));
                case 9:
                    return new Decoration(contentManager.Load<Texture2D>("level/sign"),
                        new Rectangle(x, y, tileSize, tileSize));
                case 10:
                    return new Enemy(contentManager.Load<Texture2D>("enemy/enemy-sprite"),
                        new Rectangle(x, y, 128, 132), new Vector2(1, 0),
                        (float) tileSize / 128, new Vector2(x + 3 * tileSize, y));
            }
            return null;
        }
    }
}