namespace PenguinSlide
{
    public abstract class Control
    {
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Jump { get; set; }
        public bool Slide { get; set; }
        public bool Idle { get; set; }
        public abstract void Update();
    }
}
