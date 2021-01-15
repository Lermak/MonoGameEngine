using System;

namespace MonoGame_Core.Scripts
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameManager())
                game.Run();
        }
    }
}
