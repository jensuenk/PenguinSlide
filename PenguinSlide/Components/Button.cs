using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PenguinSlide.Components;

namespace PenguinSlide.Components
{
    public class Button : Component
    {
        private MouseState currentMouse, previousMouse;
        private bool isHovering;
        public event EventHandler Click;

        public bool Clicked { get; private set; }
        
        public Button(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
        }
        public void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if(mouseRectangle.Intersects(rectangle))
            {
                isHovering = true;

                if(currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
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