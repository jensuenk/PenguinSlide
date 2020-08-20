using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;

namespace PenguinSlide.Level
{
    public class Level
    {
        private readonly ContentManager contentManager;
        private readonly int[,] map;
        private readonly int size;

        private List<ICollectable> collectables = new List<ICollectable>();
        public Rectangle Bounds { get; }

        public Level(ContentManager contentManager, Viewport viewport, int[,] map)
        {
            this.contentManager = contentManager;
            this.map = map;
            Bounds = viewport.Bounds;
            size = viewport.Height / map.GetLength(0);
            PlayerSize = size;
            Generate();
        }

        public Vector2 PlayerLocation { get; private set; }
        public int PlayerSize { get; }
        public List<Component> Components { get; } = new List<Component>();

        public Portal Portal => Components.OfType<Portal>().ToList().First();

        public List<IDamageable> Damageables => Components.OfType<IDamageable>().ToList();

        public List<Tile> Tiles => Components.OfType<Tile>().ToList();

        public List<ICollectable> ActiveCollectables => Components.OfType<ICollectable>().ToList();
        
        public int CollectablesAmount { get; private set; }

        public bool IsCompleted { get; set; } = false;

        private void Generate()
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                for (var y = 0; y < map.GetLength(0); y++)
                {
                    var number = map[y, x];
                    switch (number)
                    {
                        case 1:
                            PlayerLocation = new Vector2(x * size, y * size);
                            break;
                        case 2:
                            Components.Add(new Tile(contentManager.Load<Texture2D>("tiles/block_ground_00_single"),
                                new Rectangle(x * size, y * size, size, size)));
                            break;
                        case 3:
                            Components.Add(new Star(contentManager.Load<Texture2D>("star"),
                                new Rectangle(x * size, y * size, size, size)));
                            CollectablesAmount++;
                            break;
                        case 4:
                            Components.Add(new Spike(contentManager.Load<Texture2D>("spikes"),
                                new Rectangle(x * size, y * size + size / 2, size, size / 2)));
                            break;
                        case 5:
                            Components.Add(new Tile(contentManager.Load<Texture2D>("tiles/"),
                                new Rectangle(x * size, y * size, size, size)));
                            break;
                        case 6:
                            Components.Add(new Tile(contentManager.Load<Texture2D>("tiles/"),
                                new Rectangle(x * size, y * size, size, size)));
                            break;
                        case 7:
                            Components.Add(new Portal(contentManager.Load<Texture2D>("igloo"),
                                new Rectangle(x * size, y * size, size * 2, size)));
                            break;
                        case 8:
                            Components.Add(new Tile(contentManager.Load<Texture2D>("crate"),
                                new Rectangle(x * size, y * size, size, size)));
                            break;
                        case 9:
                            Components.Add(new Decoration(contentManager.Load<Texture2D>("sign_2"),
                                new Rectangle(x * size, y * size, size, size)));
                            break;
                    }
                }
            }
            collectables = ActiveCollectables;
        }

        public void RespawnCollectables()
        {
            foreach (var collectable in collectables.Where(collectable => !Components.Contains((Component) collectable)))
            {
                Components.Add((Component) collectable);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in Components) component.Draw(spriteBatch);
        }
    }
}