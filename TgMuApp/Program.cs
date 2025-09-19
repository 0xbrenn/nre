using Krypton.Toolkit;
using log4net.Config;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TgMuApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
            XmlConfigurator.Configure();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmWelcome());
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var exStr = e.Exception.Message;
            KryptonMessageBox.Show(exStr, "Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error, showCtrlCopy: false);

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

        }
    }
}
