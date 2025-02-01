using System;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions.Systems;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    public partial class ManualRegistrationDialog : Form
    {
        readonly ClockingSystem clockingSystem;

        bool hasSelectedDate;

        public ManualRegistrationDialog(ClockingSystem clockingSystem)
        {
            InitializeComponent();
            this.clockingSystem = clockingSystem;
            this.dateTimePicker.MaxDate = DateTime.Today.AddDays(-1);
        }

        public DateTime SelectedDate
        {
            get
            {
                return dateTimePicker.Value.Date;
            }
        }

        public TimeSpan WorkTime
        {
            get
            {
                const int lunchTimeMinutes = 30;
                var totalMinutes = ((int)hourBox.Value * 60) + (int)minuteBox.Value + lunchTimeMinutes;
                return TimeSpan.FromMinutes(totalMinutes);
            }
        }

        private void dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            var timeWorked = clockingSystem.GetTimeWorked(this.SelectedDate);
            var hasTimeWorked = timeWorked != TimeSpan.Zero;
            if (hasTimeWorked)
            {
                //NOTE: Do not change the order here...
                var hours = timeWorked.Hours;
                var minutes = timeWorked.Minutes;
                hourBox.Minimum = hours;
                hourBox.Maximum = hours;
                hourBox.Value = hours; //NOTE: Will set minuteBox's minimum & maximum
                minuteBox.Minimum = minutes;
                minuteBox.Maximum = minutes;
                minuteBox.Value = minutes;
            }
            else
            {
                //NOTE: Do not change the order here...
                hourBox.Minimum = 0;
                hourBox.Maximum = 16;
                hourBox.Value = 7; //NOTE: Will set minuteBox's minimum & maximum
                minuteBox.Value = 30;
            }
            minuteBox.ReadOnly = hasTimeWorked;
            hourBox.ReadOnly = hasTimeWorked;
            minuteBox.Enabled = true;
            hourBox.Enabled = true;
            hasSelectedDate = true;
            OKButtonEnableCheck();
        }

        private void hourBox_ValueChanged(object sender, EventArgs e)
        {
            if (hourBox.Value == 0)
            {
                minuteBox.Minimum = 0;
                minuteBox.Maximum = 60;
            }
            else
            {
                var isMaximumHour = hourBox.Value == hourBox.Maximum;
                minuteBox.Minimum = -1;
                minuteBox.Maximum = isMaximumHour ? 0 : 60;
            }
            OKButtonEnableCheck();
        }

        private void minuteBox_ValueChanged(object sender, EventArgs e)
        {
            if (minuteBox.Value == 60)
            {
                if (hourBox.Value != hourBox.Maximum)
                {
                    hourBox.Value++;
                }
                minuteBox.Value = 0;
            }
            else if (minuteBox.Value == -1)
            {
                if (hourBox.Value != hourBox.Minimum)
                {
                    hourBox.Value--;
                }
                minuteBox.Value = 59;
            }
            OKButtonEnableCheck();
        }

        private void OKButtonEnableCheck()
        {
            this.OK_Button.Enabled = hasSelectedDate && (hourBox.Value != 0 || minuteBox.Value != 0);
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
