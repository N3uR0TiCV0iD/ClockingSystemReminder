using System;
using System.Windows.Forms;
using ClockingSystemReminder.Data;

namespace ClockingSystemReminder.ClockingSystems.WinTid
{
    public partial class WeekRotationScheduleDialog : Form
    {
        public WeekRotationSchedule WeekRotationSchedule
        {
            get
            {
                return new WeekRotationSchedule(startWeekDatePicker.Value, (int)rotationIntervalBox.Value);
            }
        }

        public WeekRotationScheduleDialog(WeekRotationSchedule? rotationSchedule)
        {
            InitializeComponent();
            if (rotationSchedule != null)
            {
                startWeekDatePicker.Value = rotationSchedule.StartDate;
                rotationIntervalBox.Value = rotationSchedule.RotationWeeks;
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
