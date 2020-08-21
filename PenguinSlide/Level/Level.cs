using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Components;
using PenguinSlide.Entities;

namespace PenguinSlide.Level
{
    public class Level
    {
        private readonly ContentManager contentManager;
        private readonly LevelFactory levelFactory;
        private readonly int[,] map;

        private List<ICollectable> collectables = new List<ICollectable>();

        public Level(ContentManager contentManager, Viewport viewport, int[,] map)
        {
            this.contentManager = contentManager;
            levelFactory = new LevelFactory();
            this.map = map;
            Bounds = viewport.Bounds;
            TileSize = viewport.Height / map.GetLength(0);
            Generate();
        }

        public Rectangle Bounds { get; }

        public Vector2 PlayerLocation { get; private set; }
        public List<Enemy> Enemies => Components.OfType<Enemy>().ToList();
        public int TileSize { get; }

        public List<Component> Components { get; } = new List<Component>();

        public Portal Portal => Components.OfType<Portal>().ToList().First();

        public IEnumerable<IDamageable> Damageables => Components.OfType<IDamageable>().ToList();

        public IEnumerable<Tile> Tiles => Components.OfType<Tile>().ToList();

        public List<ICollectable> ActiveCollectables => Components.OfType<ICollectable>().ToList();

        public int CollectablesAmount { get; private set; }

        public bool IsCompleted { get; set; }

        private void Generate()
        {
            for (var x = 0; x < map.GetLength(1); x++)
            for (var y = 0; y < map.GetLength(0); y++)
            {
                var id = map[y, x];
                var component = levelFactory.CreateComponent(id, new Vector2(x, y), TileSize, contentManager);
                if (component != null)
                    Components.Add(component);

                switch (id)
                {
                    case 1:
                        PlayerLocation = new Vector2(x * TileSize, y * TileSize);
                        break;
                    case 3:
                        CollectablesAmount++;
                        break;
                }
            }

            collectables = ActiveCollectables;
        }

        public void RespawnCollectables()
        {
            foreach (var collectable in collectables.Where(collectable => !Components.Contains((Component) collectable))
            ) Components.Add((Component) collectable);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in Components) component.Draw(spriteBatch);
        }
    }
}