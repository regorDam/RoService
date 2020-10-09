using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace RoService
{
    public partial class Service1 : ServiceBase
    {
        private EventLog eventLog1;
        private ProcessStartInfo processInfo;
        private Process process;

        [DllImport("User32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public Service1()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
            processInfo = new ProcessStartInfo();
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart.");
            Run();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
            process.Close();
            process.Dispose();
            
        }

        private void Run()
        {
            
            
            eventLog1.WriteEntry("In Running.");

            processInfo.FileName = "C:\\Users\\Who\\source\\repos\\RoAppGUI\\RoAppGUI\\bin\\Release\\RoAppGUI.exe";
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            process = Process.Start(processInfo);
            IntPtr handle = process.MainWindowHandle;
            SwitchToThisWindow(handle, true);
         
            //Process on background
            //Process.Start("C:\\Users\\Who\\source\\repos\\RoAppGUI\\RoAppGUI\\bin\\Release\\RoAppGUI.exe");


        }
    }
}
