using System;

namespace PenguinSlide
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new PenguinSlide())
            {
                game.Run();
            }
        }
    }
}