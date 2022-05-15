using System;
using System.Threading;
using System.Windows.Forms;
using Chat_Together;

namespace TestSocket
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
            var t = new Thread(Start);
            t.TrySetApartmentState(ApartmentState.STA);
            //t.Start();
            Application.Run(new SendData(Dependency.ServerDependent));
        }

        private static void Start()
        {
            
        }
    }
}
