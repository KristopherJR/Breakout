using System;

namespace Breakout.Game_Code
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BreakoutGame())
                game.Run();
        }
    }
}
