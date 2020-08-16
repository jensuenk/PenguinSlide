using Microsoft.Xna.Framework;

namespace PenguinSlide
{
    public class AnimationCreator
    {
        public void Create(Animation currentAnimation, int startX, int startY, int width, int height, int frames)
        {
            for (int i = 0; i < frames; i++)
            {
                currentAnimation.AddFrame(new Rectangle(startX, startY, width, height));
                startX += width;
            }
        }
    }
}
