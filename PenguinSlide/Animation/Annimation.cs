using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PenguinSlide
{
    public class Animation
    {
        private List<AnimationFrame> frames;
        public AnimationFrame CurrentFrame;
        private double xOffset;
        private int speed = 80;
        private int counter = 0;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public Animation(int speed)
        {
            this.speed = speed;
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
            };

            frames.Add(newFrame);
            CurrentFrame = frames[0];
        }


        public void Update(GameTime gameTime)
        {
            xOffset += CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.Milliseconds / speed;
            if (xOffset >= CurrentFrame.SourceRectangle.Width)
            {
                counter++;
                if (counter >= frames.Count)
                {
                    counter = 0;
                }
                CurrentFrame = frames[counter];
                xOffset = 0;
            }
        }
    }
}
