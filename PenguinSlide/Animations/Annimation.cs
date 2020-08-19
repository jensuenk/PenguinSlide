using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PenguinSlide.Animations
{
    public class Animation
    {
        private int counter;
        public AnimationFrame CurrentFrame;
        private readonly List<AnimationFrame> frames;
        private readonly int speed = 100;
        private double xOffset;

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
            var newFrame = new AnimationFrame
            {
                SourceRectangle = rectangle
            };

            frames.Add(newFrame);
            CurrentFrame = frames[0];
        }

        public int GetFrameAmount()
        {
            return frames.Count;
        }

        public void Update(GameTime gameTime)
        {
            xOffset += CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.Milliseconds / speed;
            if (xOffset >= CurrentFrame.SourceRectangle.Width)
            {
                counter++;
                if (counter >= frames.Count) counter = 0;
                CurrentFrame = frames[counter];
                xOffset = 0;
            }
        }
    }
}