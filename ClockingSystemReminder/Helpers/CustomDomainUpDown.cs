using System;
using System.Windows.Forms;

namespace ClockingSystemReminder.Helpers
{
    //Custom control to go around the "SystemInformation.MouseWheelScrollLines" setting
    public class CustomDomainUpDown : DomainUpDown
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var scrolls = e.Delta / 120;
            if (scrolls > 0)
            {
                //Scrolled up. We need to subtract the scroll amount
                this.SelectedIndex = MoveSelectionIndex(-scrolls); //eg: MoveSelectionIndex(-1)
            }
            else if (scrolls < 0)
            {
                //Scrolled down. We need to add the scroll amount
                this.SelectedIndex = MoveSelectionIndex(-scrolls); //eg: MoveSelectionIndex(1)
            }
        }

        private int MoveSelectionIndex(int amount)
        {
            var maxIndex = this.Items.Count - 1;
            var newIndex = this.SelectedIndex + amount;
            if (newIndex < 0)
            {
                if (!this.Wrap)
                {
                    return 0;
                }

                var offset = newIndex % this.Items.Count;
                return this.Items.Count + offset;
            }
            else if (newIndex > maxIndex)
            {
                if (!this.Wrap)
                {
                    return maxIndex;
                }
                return newIndex % this.Items.Count;
            }
            return newIndex;
        }
    }
}
