using AccountLibrary;
using System;
using System.Windows.Forms;

namespace Management
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new MainForm(AccountDB.FindAccount("employee")));
            Application.Run(new MainForm(AccountDB.FindAccount("manager")));
        }
    }
}
