using System;
using System.Windows.Forms;

namespace ClockingSystemReminder.Helpers
{
    //Custom control to allow overriding the default "MouseWheelScrollLines" setting
    public class CustomNumericUpDown : NumericUpDown
    {
        public decimal LargeIncrement { get; set; } = GetDefaultLargeIncrement();

        private static decimal GetDefaultLargeIncrement()
        {
            var mouseScrollLines = SystemInformation.MouseWheelScrollLines;
            return mouseScrollLines > 0 ? mouseScrollLines : 1;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var increment = e.Delta > 0 ? this.LargeIncrement : -this.LargeIncrement;
            var newValue = this.Value + increment;
            if (newValue > this.Maximum || newValue < this.Minimum)
            {
                return;
            }
            this.Value = newValue;
        }
    }
}
