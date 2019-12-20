using CrashReporterDotNET;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Zużycie_LED_do_zlecenia
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool release = true;
#if DEBUG
                release = false;
#endif
            if (release)
            {
                Application.ThreadException += ApplicationThreadException;

                AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            SendReport((Exception)unhandledExceptionEventArgs.ExceptionObject);
            Environment.Exit(0);
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            SendReport(e.Exception);
        }

        public static void SendReport(Exception exception, string developerMessage = "", bool silent = true)
        {
            var reportCrash = new ReportCrash("piotr.dabrowski@mstechnology.pl")
            {
                DeveloperMessage = developerMessage,
                CaptureScreen = true
            };
            reportCrash.Silent = silent;
            reportCrash.Send(exception);
        }
    }
}