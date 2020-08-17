using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace PenguinSlide
{
    public class LevelManager
    {
        private ContentManager contentManager;
        
        private List<Level> levels;
        public Level CurrentLevel { get; private set; }

        public LevelManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }
        
        public void GenerateLevels()
        {
            levels = new List<Level>
            {
                new Level(contentManager,
                    new int[,] {
                        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,1,0,0,0,1,0,1,1},
                        { 0,0,0,0,0,0,0,0,0,1,1,1,0,1,0},
                        { 0,0,0,0,0,0,0,1,1,1,0,1,0,1,0},
                        { 0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                    }
                ),
                new Level(contentManager,
                    new int[,] {
                        { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                        { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                        { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                        { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                        { 2,0,0,0,0,0,0,0,0,0,0,0,0,5,3},
                        { 2,0,0,0,0,0,0,0,0,0,0,0,4,4,3},
                        { 2,0,0,0,0,0,0,0,0,0,0,4,0,0,3},
                        { 2,0,0,0,0,0,0,0,0,0,4,0,0,0,3},
                        { 2,0,0,0,0,0,0,0,0,4,0,0,0,0,3},
                        { 2,0,0,0,8,0,0,0,4,0,0,0,0,0,3},
                        { 2,0,0,0,0,0,0,4,0,0,0,0,0,0,3},
                        { 2,0,0,0,0,0,4,0,0,0,0,0,0,0,3},
                        { 2,0,0,0,0,4,0,0,0,0,0,0,0,0,3},
                        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
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

        private bool IsLastLevel(Level level)
        {
            return levels.Count >= levels.IndexOf(level) + 1;
        }

        public void NextLevel()
        {
            if (!IsLastLevel(CurrentLevel))
            {
                CurrentLevel = levels[levels.IndexOf(CurrentLevel) + 1];
            }
        }
    }
}