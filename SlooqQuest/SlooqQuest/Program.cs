using System;
using Geisha.Engine.Windows;

namespace SlooqQuest
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            WindowsApplication.Run(new SlooqQuestGame());
        }
    }
}