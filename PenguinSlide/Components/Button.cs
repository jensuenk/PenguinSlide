using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PenguinSlide.Components
{
    public class Button : Component
    {
        private MouseState currentMouse, previousMouse;
        private bool isHovering;

        public Button(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
        }

        public bool Clicked { get; private set; }
        public event EventHandler Click;

        public void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(texture, rectangle, colour);
        }
    }
}