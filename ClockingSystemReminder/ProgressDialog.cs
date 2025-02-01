using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockingSystemReminder
{
    public delegate void ProgressDelegate(string status, int progress);

    public partial class ProgressDialog : Form
    {
        public bool Success { get; set; }

        bool done;
        Func<ProgressDelegate, bool> task;

        public ProgressDialog(Func<ProgressDelegate, bool> task)
        {
            this.task = task;
            InitializeComponent();
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                this.Success = task(OnProgress);
                done = true;
                this.Invoke(() => this.Close());
            });
        }

        private void OnProgress(string status, int progress)
        {
            this.Invoke(() =>
            {
                statusLabel.Text = status;
                progressBar.Value = progress;
            });
        }

        protected override void WndProc(ref Message m)
        {
            const int SC_MOVE = 0xF010;
            const int WM_SYSCOMMAND = 0x0112;
            if (m.Msg == WM_SYSCOMMAND)
            {
                var command = m.WParam.ToInt32() & 0xFFF0;
                if (command == SC_MOVE)
                {
                    //Do nothing, ie: prevent it from moving around
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!done)
            {
                e.Cancel = true;
            }
        }
    }
}
