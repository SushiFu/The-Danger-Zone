#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace Server_win
{
    #if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Welcome on The Danger Zone Server");
            new Creator(4242);
        }
    }
    #endif
}
