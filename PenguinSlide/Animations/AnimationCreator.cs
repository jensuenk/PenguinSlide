using Microsoft.Xna.Framework;

namespace PenguinSlide.Animations
{
    public class AnimationCreator
    {
        public Animation Create(int startX, int startY, int width, int height, int frames)
        {
            Animation animation = new Animation();
            for (var i = 0; i < frames; i++)
            {
                animation.AddFrame(new Rectangle(startX, startY, width, height));
                startX += width;
            }
            return animation;
        }
    }
}