using System;
using System.Windows.Forms;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder
{
    public partial class ManualRegistrationDialog : Form
    {
        bool hasSelectedDate;
        bool handleDateSelection;

        public ManualRegistrationDialog()
        {
            InitializeComponent();
            this.dateTimePicker.MaxDate = DateTime.Now.AddDays(-1);
            this.handleDateSelection = true;
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

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (handleDateSelection)
            {
                hasSelectedDate = true;
                OKButtonEnableCheck();
            }
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
