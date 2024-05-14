using System;
using System.Windows.Forms;

namespace ClockingSystemReminder.Helpers
{
    public static class TaskDialogHelper
    {
        public static int ShowDialog(string text, string title, TaskDialogIcon icon, int defaultIndex, params TaskDialogButton[] buttons)
        {
            if (defaultIndex < 0 || defaultIndex >= buttons.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(defaultIndex));
            }

            var taskDialogPage = new TaskDialogPage()
            {
                Text = text,
                Caption = title,
                Icon = icon,
                SizeToContent = true
            };

            var buttonCollection = taskDialogPage.Buttons;
            foreach (var button in buttons)
            {
                buttonCollection.Add(button);
            }
            taskDialogPage.DefaultButton = buttons[defaultIndex];

            var result = TaskDialog.ShowDialog(taskDialogPage);
            return Array.IndexOf(buttons, result);
        }
    }
}
