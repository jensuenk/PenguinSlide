using Microsoft.Xna.Framework.Input;

namespace PenguinSlide.Controls
{
    public class KeyboardControl : Control
    {
        public override void Update()
        {
            var stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(Keys.Left))
                Left = true;
            if (stateKey.IsKeyUp(Keys.Left))
                Left = false;
            if (stateKey.IsKeyDown(Keys.Right))
                Right = true;
            if (stateKey.IsKeyUp(Keys.Right))
                Right = false;
            if (stateKey.IsKeyDown(Keys.Up))
                Jump = true;
            if (stateKey.IsKeyUp(Keys.Up))
                Jump = false;
            if (stateKey.IsKeyDown(Keys.Down))
                Slide = true;
            if (stateKey.IsKeyUp(Keys.Down))
                Slide = false;
            Idle = stateKey.GetPressedKeys().Length == 0;
        }
    }
}