using System;
using System.Windows.Forms;
using ClockingSystemReminder.Data;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace ClockingSystemReminder.ClockingSystems.WinTid
{
    public partial class WinTidSettingsDialog : Form
    {
        public WeekRotationSchedule WeekRotationSchedule
        {
            get
            {
                if (enable24_7DutyBox.Checked)
                {
                    return null;
                }
                return new WeekRotationSchedule(startWeekDatePicker.Value, (int)rotationIntervalBox.Value);
            }
        }

        public WinTidSettingsDialog(WinTidSettings settings)
        {
            InitializeComponent();

            var rotationSchedule = settings.WeekRotationSchedule;
            if (rotationSchedule != null)
            {
                startWeekDatePicker.Value = rotationSchedule.StartDate;
                rotationIntervalBox.Value = rotationSchedule.RotationWeeks;
                enable24_7DutyBox.Enabled = true;
            }
        }

        private void enable24_7DutyBox_CheckedChanged(object sender, EventArgs e)
        {
            dutyWeeksGroupBox.Enabled = enable24_7DutyBox.Checked;
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
