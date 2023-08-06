using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ClockingSystemReminder.Helpers;
using ClockingSystemReminder.Properties;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    public class ClockingSystemForm : ShutdownBlockerForm
    {
        const int TIMEWORKED_REFRESH_DELAY = 5000;

        NotifyIcon trayIcon;
        Stopwatch lastRefreshStopwatch;
        ToolStripMenuItem clockActionMenuItem;
        ToolStripMenuItem scheduleClockActionMenuItem;

        public ClockingSystemForm() : base("WARNING: You have not clocked-out yet!", Resources.Icon)
        {
            this.lastRefreshStopwatch = Stopwatch.StartNew();

            clockActionMenuItem = new ToolStripMenuItem("Clock-Out");
            clockActionMenuItem.Click += clockActionItem_Click;

            scheduleClockActionMenuItem = new ToolStripMenuItem("Schedule Clock-Out");
            scheduleClockActionMenuItem.Click += scheduleClockOutMenuItem_Click;

            ToolStripMenuItem settingsMenuItem = new ToolStripMenuItem("Settings");
            settingsMenuItem.Click += settingsMenuItem_Click;

            ToolStripMenuItem closeMenuItem = new ToolStripMenuItem("Close");
            closeMenuItem.Font = new Font(closeMenuItem.Font, FontStyle.Bold);
            closeMenuItem.Click += exitActionItem_Click;

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(clockActionMenuItem);
            menu.Items.Add(new ToolStripSeparator());

            menu.Items.Add(scheduleClockActionMenuItem);
            menu.Items.Add(settingsMenuItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(closeMenuItem);

            trayIcon = new NotifyIcon();
            trayIcon.Icon = Resources.Icon;
            trayIcon.ContextMenuStrip = menu;
            trayIcon.MouseMove += trayIcon_MouseMove;
            trayIcon.Text = "ClockingSystemReminder";
            trayIcon.Visible = true;
        }

        public void OnClockIn()
        {
            base.BlockShutdown();
            SetClockActionText("Out");
        }

        public void OnClockOut()
        {
            base.UnblockShutdown();
            SetClockActionText("In");
        }

        private void SetClockActionText(string clockAction)
        {
            scheduleClockActionMenuItem.Text = "Schedule Clock-" + clockAction;
            clockActionMenuItem.Text = "Clock-" + clockAction;
        }

        private void trayIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastRefreshStopwatch.ElapsedMilliseconds >= TIMEWORKED_REFRESH_DELAY)
            {
                lastRefreshStopwatch.Reset();
                lastRefreshStopwatch.Start();
                trayIcon.Text = "ClockingSystemReminder\nTime Worked: " + FormatTimeSpan(ClockingManager.TimeWorked);
            }
        }

        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void clockActionItem_Click(object sender, EventArgs e)
        {
            if (ClockingManager.ClockedIn) //Are we currently clocked in?
            {
                ClockingManager.ClockOutProcess();
            }
            else
            {
                ClockingManager.ClockInProcess();
            }
        }

        private void scheduleClockOutMenuItem_Click(object sender, EventArgs e)
        {
            using (var timeDialog = new ScheduleTimeDialog())
            {
                if (timeDialog.ShowDialog() == DialogResult.OK)
                {
                    var scheduledTime = timeDialog.DateTime;
                    ClockingManager.ScheduledClockActionTime = scheduledTime;
                    MessageBox.Show($"{clockActionMenuItem.Text} has been scheduled @ {scheduledTime:HH:mm}",
                                    "Schedule successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            ClockingManager.OpenSettingsDialog();
        }

        private void exitActionItem_Click(object sender, EventArgs e)
        {
            if (CanClose())
            {
                ClockingManager.Terminate();
                trayIcon.Visible = false;
                this.Close();
            }
        }

        private bool CanClose()
        {
            return !ClockingManager.ClockedIn || MessageBox.Show("WARNING: You have not clocked out yet!\n\nAre you sure you wish to close the app?",
                                                                 "WARNING: Not clocked out yet!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
