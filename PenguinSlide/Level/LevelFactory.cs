using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;

namespace PenguinSlide.Level
{
    public class LevelFactory
    {
        public Component CreateComponent(int id, Vector2 position, int size, ContentManager contentManager)
        {
            var x = (int)position.X;
            var y = (int)position.Y;
            
            switch (id)
            {
                case 2:
                    return new Tile(contentManager.Load<Texture2D>("level/ice-tile"),
                        new Rectangle(x * size, y * size, size, size));
                case 3:
                    return new Star(contentManager.Load<Texture2D>("level/star"),
                        new Rectangle(x * size, y * size, size, size));
                case 4:
                    return new Spike(contentManager.Load<Texture2D>("level/spikes"),
                        new Rectangle(x * size, y * size + size / 2, size, size / 2));
                case 5:
                    return new Decoration(contentManager.Load<Texture2D>("level/tree"),
                        new Rectangle(x * size, y * size, size * 2, size * 2));
                case 6:
                    return new Tile(contentManager.Load<Texture2D>("level/ice-tile-clear"),
                        new Rectangle(x * size, y * size, size, size));
                case 7:
                    return new Portal(contentManager.Load<Texture2D>("level/igloo"),
                        new Rectangle(x * size, y * size, size * 2, size));
                case 8:
                    return new Tile(contentManager.Load<Texture2D>("level/crate"),
                        new Rectangle(x * size, y * size, size, size));
                case 9:
                    return new Decoration(contentManager.Load<Texture2D>("level/sign"),
                        new Rectangle(x * size, y * size, size, size));
            }
            return null;
        }
    }
}