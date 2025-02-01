using System;
using System.Windows.Forms;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    public partial class ScheduleTimeDialog : Form
    {
        public ScheduleTimeDialog()
        {
            var now = DateTime.Now;
            InitializeComponent();
            this.hourBox.Minimum = now.Hour;
            this.hourBox.Value = now.Hour + 1;
            this.minuteBox.Value = now.Minute;
        }

        public DateTime DateTime
        {
            get
            {
                int totalMinutes = (int)((hourBox.Value * 60) + minuteBox.Value);
                return DateTime.Today.AddMinutes(totalMinutes);
            }
        }

        private void hourBox_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            bool isCurrentHour = hourBox.Value == now.Hour;
            if (hourBox.Value < now.Hour ||
                (isCurrentHour && now.Minute == 59))
            {
                hourBox.Minimum = now.Hour + 1;
                minuteBox.Minimum = -1;
            }
            else if (isCurrentHour)
            {
                minuteBox.Minimum = Math.Min(now.Minute + 1, 59);
            }
            else
            {
                minuteBox.Minimum = -1;
            }
        }

        private void minuteBox_ValueChanged(object sender, EventArgs e)
        {
            if (minuteBox.Value == 60)
            {
                minuteBox.Value = 0;
                if (hourBox.Value != hourBox.Maximum)
                {
                    hourBox.Value++;
                }
            }
            else if (minuteBox.Value == -1)
            {
                minuteBox.Value = 59;
                if (hourBox.Value != hourBox.Minimum)
                {
                    hourBox.Value--;
                }
            }
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
