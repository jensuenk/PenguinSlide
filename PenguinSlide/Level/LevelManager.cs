using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PenguinSlide.Level
{
    public class LevelManager
    {
        private readonly ContentManager contentManager;
        private readonly Viewport viewport;
        private List<Level> levels;

        public LevelManager(ContentManager contentManager, Viewport viewport)
        {
            this.contentManager = contentManager;
            this.viewport = viewport;
        }

        public Level CurrentLevel { get; set; }

         public void GenerateLevels()
        {
            levels = new List<Level>
            {
                new Level(contentManager, viewport, new[,]
                    {
                        {2,2,2,2,2,2,2,0,0,0,0,0,2,2,2,2,2,0,0,0,0,0,6,6,0,0,0,5,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,2,2,2,0,0,3,0,0,2,2,2,2,2,0,0,0,0,0,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,2,2,0,0,0,0,0,0,0,2,2,2,0,0,0,9,0,0,6,6,0,0,0,2,2,0,0,0,0,3,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,2,0,0,0,6,6,6,0,0,0,0,0,0,0,2,2,2,0,6,6,6,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,6,6,6,2,2,2,2},
                        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,6,6,0,0,0,2,2,2,2,2,0,0,0,0,6,6,0,0,0,0,2,2,2,2,2,2,2,2,6,6,6,6,2,2,2,2},
                        {0,0,9,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,0,0,0,6,6,0,3,0,2,2,2,2,2,0,0,0,0,6,6,6,0,0,0,0,0,0,0,2,2,2,2,6,6,6,6,2,2,2,2},
                        {2,2,2,0,6,0,0,5,0,0,0,0,2,2,2,2,2,2,2,0,6,0,6,6,0,0,2,2,2,2,2,0,0,0,0,0,6,6,6,0,0,0,9,0,7,0,2,2,2,2,2,2,6,6,2,2,2,2},
                        {2,2,2,3,6,6,0,0,0,0,0,0,0,0,0,0,0,2,2,0,6,3,6,6,0,0,0,2,2,2,0,0,0,0,0,0,6,6,6,0,0,2,2,2,2,2,2,2,2,2,2,2,6,6,2,2,2,2},
                        {2,2,2,0,6,6,6,6,6,6,0,0,0,0,0,0,0,2,2,0,6,6,6,6,0,0,0,2,2,2,0,0,0,0,0,0,6,6,6,0,0,2,2,2,2,2,2,2,2,2,2,2,6,6,2,2,2,2},
                        {2,2,0,0,0,6,6,6,6,6,6,6,6,6,6,0,0,2,2,0,6,6,6,6,6,0,0,2,2,2,0,0,0,0,0,0,6,6,6,0,0,2,2,2,2,2,2,2,2,2,6,6,6,6,6,2,2,2},
                        {5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,6,6,6,0,0,2,2,2,2,2,2,2,2,2,6,6,6,6,6,2,2,2},
                        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,2,2,0,0,0,0,0,0,0,8,2,2,2,0,0,0,0,0,0,6,6,6,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,0,0,3,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,4,0,8,8,2,2,2,0,0,0,0,0,0,6,6,6,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,4,4,2,4,4,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,4,4,4,4,4,4,6,6,6,4,4,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
                    }),
                new Level(contentManager, viewport,new[,]
                    {
                        {2,2,0,0,0,0,0,0,0,6,6,6,6,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,0,3,0,0,0,0,0,6,6,6,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,2,0,0,0,0,6,6,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,0,0,2,2,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2},
                        {2,2,2,2,0,0,0,0,6,6,6,6,0,0,0,0,0,0,0,0,6,0,0,0,6,0,0,0,0,0,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,2,2,2,2,6,6,6,6,2,2,2,2},
                        {2,2,2,0,0,0,6,6,6,6,6,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,4,0,0,3,2,2,0,0,0,0,0,0,0,0,0,9,0,0,7,0,2,2,2,6,6,6,6,6,6,2,2,2},
                        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,0,3,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,6,6,2,2,6,6,2,2,2},
                        {0,0,0,9,0,0,0,0,0,0,0,0,0,0,6,6,6,6,6,6,0,0,0,0,0,0,2,2,2,2,2,2,6,6,6,6,0,0,0,2,2,2,2,2,2,2,2,2,2,6,6,2,6,6,6,2,2,2},
                        {2,2,2,2,0,0,5,0,0,0,0,0,0,6,6,6,6,6,6,6,6,0,0,0,0,0,0,0,0,2,2,2,6,6,6,0,0,0,2,2,0,0,0,3,2,2,2,2,2,2,2,6,6,6,2,2,2,2},
                        {2,2,2,2,2,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,2,2,2,6,6,0,0,0,2,2,0,0,6,0,0,2,2,2,2,2,2,6,6,6,2,2,2,2,2},
                        {2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,9,0,0,0,0,0,0,8,8,4,0,0,0,2,2,2,6,0,0,0,2,2,0,0,6,6,0,0,2,2,2,2,2,6,6,6,6,6,6,2,2,2},
                        {2,2,2,2,2,2,2,2,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,2,2,0,0,0,2,2,0,0,6,6,6,0,0,2,2,2,2,2,6,6,6,6,6,6,2,2,2},
                        {2,2,2,2,2,2,2,2,2,2,0,0,0,2,2,2,0,3,0,2,2,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {0,0,0,0,0,0,0,0,2,2,4,4,4,2,2,2,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,0,0,0,4,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                        {0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
                    })
            };
            CurrentLevel = levels[0];
        }

        public Level GetLevel(int number)
        {
            return levels[number];
        }

        public Level GetNextLevel()
        {
            return IsLastLevel(CurrentLevel) ? null : levels[levels.IndexOf(CurrentLevel) + 1];
        }

        public bool IsLastLevel(Level level)
        {
            return level == levels.Last();
        }
    }
}