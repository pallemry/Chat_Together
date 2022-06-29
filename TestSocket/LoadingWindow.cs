using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

using Timer = System.Timers.Timer;
#nullable enable
namespace TestSocket
{
    public partial class LoadingWindow : Form
    {
        public int MinimumProgressBarInterval { get; } = 1;
        public int MaximumProgressBarInterval { get; } = 20;
        private readonly Form? _parentForm;

        public LoadingWindow()
        {
            _parentForm = null;
            InitializeComponent();
            BringToFront();
            TopMost = true;
        }
        public LoadingWindow(Form parentForm)
        {
            _parentForm = parentForm;
            InitializeComponent();
            CenterToScreen();
            BringToFront();
            TopMost = true;
        }
        private Timer? _t;
        private void LoadingWindow_Load(object sender, EventArgs e)
        {
            _t = new Timer(new Random().Next(MinimumProgressBarInterval, MaximumProgressBarInterval));
            _t.Elapsed += EvaluateProgressbar;
            _t.Start();


            Cursor = Cursors.WaitCursor;
            if (_parentForm != null)
            {
                Invoke(delegate() {
                    _parentForm.Cursor = Cursors.WaitCursor;
                    Debug.WriteLine($"Loading window: parent form name: {_parentForm.Text}, cursor: {_parentForm.Cursor}");
                });
            }
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
            _t = new Timer(new Random().Next(MinimumProgressBarInterval, MaximumProgressBarInterval));
            _t.Elapsed += EvaluateProgressbar;
            _t.Start();
        }

        private void LoadingWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _t = null;
            Invoke(() => _parentForm?.ResetCursor());
        }
    }
}