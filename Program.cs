using System;
using System.Windows.Forms;

namespace CsharpInterface
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GraphRealTime.Form1());
            Application.Run(new Form1());
        }
    }
}
