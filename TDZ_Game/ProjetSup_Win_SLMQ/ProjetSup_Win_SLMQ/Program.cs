#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace ProjetSup_Win_SLMQ
{
    #if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameMain())
                game.Run();
        }
    }
    #endif
}
