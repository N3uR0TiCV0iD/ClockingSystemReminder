using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#pragma warning disable SA1134 // Attributes should not share line
namespace ClockingSystemReminder.Helpers
{
    public class ShutdownBlockerForm : Form
    {
        [DllImport("user32.dll")] private static extern bool ShutdownBlockReasonDestroy(IntPtr hwnd);
        [DllImport("user32.dll")] private static extern bool ShutdownBlockReasonCreate(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string reason);
        [DllImport("kernel32.dll")] private static extern bool SetProcessShutdownParameters(uint dwLevel, uint dwFlags);

        bool blocking;
        string blockReason;

        //A parameterless constructor is needed for the designer to work with forms that inherit us
        private ShutdownBlockerForm()
        {
        }

        public ShutdownBlockerForm(string blockReason, Icon icon)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.WindowState = FormWindowState.Minimized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.blockReason = blockReason;
            this.Text = "ShutdownBlocker";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Icon = icon;
            this.Height = 0;
            this.Width = 0;
        }

        public string BlockReason
        {
            get => blockReason;
            set
            {
                blockReason = value;
                if (!string.IsNullOrEmpty(blockReason))
                {
                    BlockShutdown();
                }
                else
                {
                    UnblockShutdown();
                }
            }
        }

        public bool BlockingShutdown
        {
            get => blocking;
            set
            {
                if (blocking != value)
                {
                    if (value)
                    {
                        BlockShutdown();
                    }
                    else
                    {
                        UnblockShutdown();
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            const uint SHUTDOWN_NORETRY = 0x01;
            SetProcessShutdownParameters(0x2BE, SHUTDOWN_NORETRY);
            BlockShutdown();
            base.OnLoad(e);
        }

        public bool BlockShutdown()
        {
            if (!blocking && !string.IsNullOrEmpty(blockReason))
            {
                blocking = ShutdownBlockReasonCreate(this.Handle, blockReason);
            }
            return blocking;
        }

        public bool UnblockShutdown()
        {
            if (blocking && ShutdownBlockReasonDestroy(this.Handle))
            {
                blocking = false;
            }
            return !blocking;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_ENDSESSION = 0x16;
            const int WM_QUERYENDSESSION = 0x11;
            if (!blocking || (m.Msg != WM_QUERYENDSESSION && m.Msg != WM_ENDSESSION))
            {
                base.WndProc(ref m);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ShutdownBlockReasonDestroy(this.Handle);
        }
    }
}
#pragma warning restore SA1134 // Attributes should not share line
