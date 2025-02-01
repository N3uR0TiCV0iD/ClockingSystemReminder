using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClockingSystemReminder.Helpers
{
    public static class AutoStartupHelper
    {
        public static void CheckAutoStartup(RegistryKey appRegistryKey, string startupName)
        {
            using (var startupRegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                var startupPath = startupRegistryKey!.GetValue(startupName);
                if (startupPath == null)
                {
                    var checkedAutoStartup = appRegistryKey.GetValue("CheckedAutoStartup");
                    if (checkedAutoStartup == null || (int)checkedAutoStartup == 0)
                    {
                        if (MessageBox.Show("Looks like this is the first time you run this application!\n\nWould you like to add it to the Windows startup?",
                                            "Add app to startup?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            AddAppToStartup(startupRegistryKey, startupName);
                        }
                        appRegistryKey.SetValue("CheckedAutoStartup", 1, RegistryValueKind.DWord);
                    }
                }
                else if (startupPath.ToString() != Application.ExecutablePath)
                {
                    //Why the "TaskDialogButton.Yes/Ignore.Text":
                    //https://github.com/dotnet/winforms/blob/main/src/System.Windows.Forms/src/System/Windows/Forms/TaskDialogPage.cs#L846
                    //https://github.com/kpreisser/winforms/issues/5#issuecomment-584318609
                    var buttons = new TaskDialogButton[]
                    {
                        new TaskDialogButton(TaskDialogButton.Ignore.Text),
                        new TaskDialogButton("No, delete it"),
                        new TaskDialogButton(TaskDialogButton.Yes.Text)
                    };
                    var result = TaskDialogHelper.ShowDialog("Looks like you've moved the app since it was last added to the Windows startup...\n\nWould you like to add it to the Windows startup again?",
                                                             "App has been moved", TaskDialogIcon.Warning, 2, buttons);
                    if (result == 2)
                    {
                        AddAppToStartup(startupRegistryKey, startupName);
                    }
                    else if (result == 1)
                    {
                        startupRegistryKey.DeleteValue(startupName);
                    }
                }
            }
        }

        private static void AddAppToStartup(RegistryKey startupRegistryKey, string startupName)
        {
            startupRegistryKey.SetValue(startupName, Application.ExecutablePath, RegistryValueKind.String);
        }
    }
}
