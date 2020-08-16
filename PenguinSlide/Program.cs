using System;

namespace PenguinSlide
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new PenguinSlide())
                game.Run();
        }
    }
}