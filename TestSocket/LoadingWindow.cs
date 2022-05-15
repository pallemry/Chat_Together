using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

using Timer = System.Timers.Timer;

namespace TestSocket
{
    public partial class LoadingWindow : Form
    {
        public int MinimumProgressBarInterval { get; set; } = 1;
        public int MaximumProgressBarInterval { get; set; } = 20;
        public LoadingWindow()
        {
            InitializeComponent();
            BringToFront();
            TopMost = true;
        }
        public LoadingWindow(Form parent)
        {
            Location = new Point(parent.Location.X / 2, parent.Location.Y / 2);
            PointToScreen(new Point(parent.Location.X / 2, parent.Location.Y / 2));
            InitializeComponent();
            BringToFront();
            TopMost = true;
        }
#nullable enable
        private Timer? _t;
#nullable disable
        private void LoadingWindow_Load(object sender, EventArgs e)
        {
            _t = new System.Timers.Timer(new Random().Next(MinimumProgressBarInterval, MaximumProgressBarInterval));
            _t.Elapsed += EvaluateProgressbar;
            _t.Start();
        }

        private void EvaluateProgressbar(object sender , ElapsedEventArgs e)
        {
            if (_t != null)
                _t?.Stop();
            else 
                return;

            try
            {
                Invoke((MethodInvoker) delegate {
                    progressBar1.Value += progressBar1.Value >= progressBar1.Maximum ? 0 : 1;
                });
            }
            catch (InvalidOperationException)
            {
                _t?.Stop();
                return;
            }
            _t = new System.Timers.Timer(new Random().Next(MinimumProgressBarInterval, MaximumProgressBarInterval));
            _t.Elapsed += EvaluateProgressbar;
            _t.Start();
        }

        private void LoadingWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _t = null;
        }
    }
}
