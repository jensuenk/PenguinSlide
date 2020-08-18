using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PenguinSlide.Entities;

namespace PenguinSlide.LevelComponents
{
    public class Camera
    {
        private Viewport viewport;
        public Matrix Transform { get; private set; }
        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }
        public void Follow(Player player)
        {
            var position = Matrix.CreateTranslation(-player.Position.X, -viewport.Height * 0.5f, 0);
            
            var offset = Matrix.CreateTranslation((player.Position.X + viewport.Width * 0.5f)  * 0.5f, viewport.Height * 0.5f, 0);

            Transform = position * offset;
        }
    }
}