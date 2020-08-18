namespace PenguinSlide.Controls
{
    public abstract class Control
    {
        public bool Left { get; protected set; }
        public bool Right { get; protected set; }
        public bool Jump { get; protected set; }
        public bool Slide { get; protected set; }
        public bool Idle { get; protected set; }
        public abstract void Update();
    }
}
