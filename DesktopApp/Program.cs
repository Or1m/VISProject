using BusinessLayer.BusinessObjects;
using System;
using System.Windows.Forms;

namespace DesktopApp
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

            var daily = DailyStatistics.Instance;

            Application.Run(new MainForm());
        }
    }
}
