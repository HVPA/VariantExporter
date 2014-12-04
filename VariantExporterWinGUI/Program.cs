using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace VariantExporterWinGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // check if another instance of this app is already running
            Mutex mutex = new Mutex(false, "HVP Variant Exporter");

            // wait 5 seconds if contended - in case another instance
            // of the program is in the process of shutting down
            if (!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
