using System;
using System.Media;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ClockingSystemReminder.Abstractions;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.ClockingSystems;
using ClockingSystemReminder.CollaborationSystems;
using ClockingSystemReminder.Data;
using ClockingSystemReminder.Extensions;
using ClockingSystemReminder.Helpers;
using ClockingSystemReminder.TicketingSystems;
using Microsoft.Win32;

namespace ClockingSystemReminder
{
    public static class ClockingManager
    {
        public const int WORK_HOURS = 8;
        const int MAX_DECLINES = 3;
        const int POLLING_DELAY = 15 * 1000; //15 seconds

        public static DateTime? ScheduledClockActionTime { get; set; }

        public static ClockingSystem ClockingSystem => clockingSystem;
        public static bool ClockedIn { get; private set; }

        static bool running;
        static ClockingSystemForm systemForm;
        static OffsetStopwatch workStopwatch;
        static ClockingSystem clockingSystem;
        static ITicketingSystem ticketingSystem;
        static ICollaborationSystem collaborationSystem;

        public static TimeSpan TimeWorked
        {
            get
            {
#if DEBUG
                if (workStopwatch == null)
                {
                    workStopwatch = new OffsetStopwatch();
                }
#endif
                return workStopwatch.Elapsed;
            }
        }

        public static bool Init(RegistryKey appRegistryKey)
        {
            var systemsLoaded = LoadSystems(appRegistryKey);
            if (systemsLoaded)
            {
                return true;
            }
            return OpenSettingsDialog();
        }

        private static bool LoadSystems(RegistryKey appRegistryKey)
        {
            clockingSystem = LoadSystem(appRegistryKey, "ClockingSystem", ClockingSystemResolver.Resolve);
            ticketingSystem = LoadSystem(appRegistryKey, "TicketingSystem", TicketingSystemResolver.Load);
            collaborationSystem = LoadSystem(appRegistryKey, "CollaborationSystem", CollaborationSystemResolver.Load);

            return clockingSystem != null &&
                   ticketingSystem != null && ticketingSystem.IsConfigured() &&
                   collaborationSystem != null && collaborationSystem.IsConfigured();
        }

        private static T LoadSystem<T>(RegistryKey appRegistryKey, string systemType, Func<string, T> systemLoader) where T : class
        {
            var systemName = (string)appRegistryKey.GetValue(systemType);
            if (string.IsNullOrEmpty(systemName))
            {
                return null;
            }
            return systemLoader(systemName);
        }

        public static bool OpenSettingsDialog()
        {
            using (var settingsDialog = new SettingsDialog(clockingSystem, ticketingSystem, collaborationSystem))
            {
                if (settingsDialog.ShowDialog() == DialogResult.OK)
                {
                    clockingSystem = settingsDialog.ClockingSystem;
                    ticketingSystem = settingsDialog.TicketingSystem;
                    collaborationSystem = settingsDialog.CollaborationSystem;
                    SaveSettings();
                    return true;
                }
                return false;
            }
        }

        private static void SaveSettings()
        {
            using (var appRegistryKey = Registry.CurrentUser.OpenSubKey(Program.REGISTRY_KEY_PATH, true))
            {
                appRegistryKey.SetValue("ClockingSystem", clockingSystem.FriendlySystemName);
                appRegistryKey.SetValue("CollaborationSystem", collaborationSystem.GetType().Name);
                appRegistryKey.SetValue("TicketingSystem", ticketingSystem.GetType().Name);
            }
        }

        public static bool ClockInProcess()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to automatically clock-in?",
                                                        "Auto clock-in?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            var autoClockIn = dialogResult == DialogResult.Yes;
            return ClockInProcess(autoClockIn);
        }

        private static bool ClockInProcess(bool autoClockIn)
        {
            if (autoClockIn)
            {
                return AutoClockIn();
            }
            return ManualClockInPrompt();
        }

        private static bool AutoClockIn()
        {
            if (AutoClockAction("in", clockingSystem.ClockIn, ManualClockInPrompt))
            {
                OnClockIn();
                return true;
            }
            return false;
        }

        private static bool AutoClockAction(string clockAction, Func<bool> actionMethod, Func<bool> manualActionMethod)
        {
            string errorReason = "UNKNOWN"; //IN THEORY it will never show "UNKNOWN"...
            bool retry = true;
            while (retry)
            {
                try
                {
                    //TODO: REFACTOR ME!!!
                    if (!DoLogin())
                    {
                        errorReason = "Login failed";
                    }
                    else if (!actionMethod())
                    {
                        errorReason = $"Server refused clock-{clockAction} request";
                    }
                    else
                    {
                        MessageBox.Show($"Auto clock-{clockAction} was successful!", "Successful clock-" + clockAction, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
                catch (WebException ex)
                {
                    if (!WebUtils.ShowNetworkRetryMessage(ex))
                    {
                        errorReason = "Network error";
                        retry = false;
                    }
                }
            }
            MessageBox.Show($"There was an error while trying to automatically clock you {clockAction}!\nBecause of this, you will need to manually clock-{clockAction}" +
                            $"\n\nReason: {errorReason}", $"Auto clock-{clockAction} failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return manualActionMethod();
        }

        private static bool DoLogin()
        {
            bool success;
            using (var systemRegistryKey = clockingSystem.OpenRegistryKey())
            {
                var credentials = clockingSystem.GetStoredCredentials(systemRegistryKey);
                var hasLogin = credentials != null;
                if (!hasLogin)
                {
                    credentials = OpenLoginForm();
                    if (credentials == null)
                    {
                        //The user cancelled the dialog...
                        return false;
                    }
                }
                success = clockingSystem.Login(credentials);
                if (!success)
                {
                    var retry = true;
                    if (hasLogin)
                    {
                        clockingSystem.DropCredentials(systemRegistryKey);
                        hasLogin = false;
                    }
                    while (retry)
                    {
                        MessageBox.Show("Invalid login credentials!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        credentials = OpenLoginForm();
                        retry = credentials != null; //Stay in the loop as long as the user confirms the dialog
                        if (retry)
                        {
                            success = clockingSystem.Login(credentials);
                            retry = !success;
                        }
                    }
                }
                if (!hasLogin && success)
                {
                    clockingSystem.SaveCredentials(credentials, systemRegistryKey);
                }
            }
            return success;
        }

        private static BasicCredentials OpenLoginForm()
        {
            using (AbstractLoginDialog loginDialog = clockingSystem.CreateLoginDialog())
            {
                if (systemForm != null) //Is the system form running?
                {
                    systemForm.Invoke(() => loginDialog.Show());
                }
                else
                {
                    Application.Run(loginDialog);
                }
                if (loginDialog.DialogResult == DialogResult.OK)
                {
                    return clockingSystem.GetCredentialsFromLoginDialog(loginDialog);
                }
            }
            return null;
        }

        public static void StartService(RegistryKey appRegistryKey, DateTime lastClockIn)
        {
            systemForm = new ClockingSystemForm();
            using (systemForm)
            {
                var timeWorking = GetTimeWorking(appRegistryKey, lastClockIn);
                var clockOutReminderThread = new Thread(() => ClockOutReminderService(timeWorking));
                clockOutReminderThread.Start();
                Application.Run(systemForm);
            }
        }

        private static TimeSpan GetTimeWorking(RegistryKey appRegistryKey, DateTime lastClockIn)
        {
            return (DateTime.Now - lastClockIn) + appRegistryKey.GetTimeSpanValue("TimeWorked");
        }

        private static void ClockOutReminderService(TimeSpan timeWorking)
        {
            workStopwatch = new OffsetStopwatch(timeWorking);
            workStopwatch.Start();
            ClockedIn = true; //Since we just started the service, we assume we should be clocked in
            running = true;
            while (running && workStopwatch.Elapsed.TotalHours < WORK_HOURS)
            {
                CheckClockActionSchedule();
                Thread.Sleep(POLLING_DELAY);
            }
            if (running && ClockedIn && ClockOutProcess_FromService())
            {
                systemForm.Invoke(() => systemForm.Close());
            }
        }

        private static void CheckClockActionSchedule()
        {
            if (DateTime.Now >= ScheduledClockActionTime)
            {
                if (ClockedIn)
                {
                    ClockOutProcess_FromService();
                }
                else
                {
                    ClockInProcess_FromService();
                }
                ScheduledClockActionTime = null; //The schedule has been triggered, let's set the scheduled time back to "null" :)
            }
        }

        private static bool ClockOutProcess_FromService()
        {
            return ClockProcess_FromService("out", ClockOutProcess);
        }

        private static bool ClockInProcess_FromService()
        {
            return ClockProcess_FromService("in", ClockInProcess);
        }

        private static bool ClockProcess_FromService(string clockAction, Func<bool, bool> clockProcessMethod)
        {
            //Here we use the systemForm in order to bring the messagebox as a "top-most" window
            bool autoAction;
            var dialogResult = DialogResult.No;
            systemForm.Invoke(() =>
            {
                SystemSounds.Beep.Play();
                systemForm.ShowInTaskbar = true;
                dialogResult = MessageBox.Show(systemForm, $"Looks like it's time to clock-{clockAction}!\n\nDo you want to automatically clock-{clockAction}?",
                                               $"Auto clock-{clockAction}?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                systemForm.ShowInTaskbar = false;
            });
            autoAction = dialogResult == DialogResult.Yes;
            return clockProcessMethod(autoAction);
        }

        private static void OnClockIn()
        {
            ClockedIn = true;
            workStopwatch?.Start();
            systemForm?.Invoke(() => systemForm.OnClockIn());
            using (RegistryKey appRegistryKey = Registry.CurrentUser.OpenSubKey(Program.REGISTRY_KEY_PATH, true))
            {
                var now = DateTime.Now;
                var lastClockIn = appRegistryKey.GetDateTimeValue("LastClockIn");
                var hasClockedInToday = lastClockIn.Date == now.Date;
                if (!hasClockedInToday) //Is this the first clock-in today?
                {
                    appRegistryKey.SetTimeSpanValue("TimeWorked", TimeSpan.Zero); //Reset the time spent working
                }
                appRegistryKey.SetDateTimeValue("LastClockIn", now);
            }
            if (!clockingSystem.OnPostClockIn())
            {
                MessageBox.Show("The Post-ClockIn process has failed!",
                                "Post-ClockIn failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void OnClockOut()
        {
            ClockedIn = false;
            workStopwatch.Stop();
            using (RegistryKey appRegistryKey = Registry.CurrentUser.OpenSubKey(Program.REGISTRY_KEY_PATH, true))
            {
                appRegistryKey.SetDateTimeValue("LastClockOut", DateTime.Now);
                appRegistryKey.SetTimeSpanValue("TimeWorked", workStopwatch.Elapsed);
            }
            systemForm.Invoke(() =>
            {
                systemForm.OnClockOut();
                OpenTimeRegistration(DateTime.Today, ClockingManager.TimeWorked);
            });
            if (!clockingSystem.OnPostClockOut())
            {
                MessageBox.Show("The Post-ClockOut process has failed!",
                                "Post-ClockOut failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void OpenTimeRegistration(DateTime registerDate, TimeSpan workTime)
        {
            using (var timeRegistrationForm = new TimeRegistrationForm(registerDate, workTime, collaborationSystem, ticketingSystem))
            {
                timeRegistrationForm.ShowDialog();
            }
        }

        public static bool ClockOutProcess()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to automatically clock-out?",
                                                        "Auto clock-out?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool autoClockOut = dialogResult == DialogResult.Yes;
            return ClockOutProcess(autoClockOut);
        }

        private static bool ClockOutProcess(bool autoClockOut)
        {
            if (autoClockOut)
            {
                return AutoClockOut();
            }
            return ManualClockOutPrompt();
        }

        private static bool AutoClockOut()
        {
            if (AutoClockAction("out", clockingSystem.ClockOut, ManualClockOutPrompt))
            {
                OnClockOut();
                return true;
            }
            return false;
        }

        private static bool ManualClockInPrompt()
        {
            //TODO: use "don't need to work __more__" if we have already clocked in?
            const string ABORT_CAPTION = "No work, no worries!";
            const string ABORT_MESSAGE = "Guess you don't need to work today then... Have a nice day! :)";
            if (ManualClockActionPrompt("in", ABORT_MESSAGE, ABORT_CAPTION))
            {
                OnClockIn();
                return true;
            }
            return false;
        }

        private static bool ManualClockOutPrompt()
        {
            const string ABORT_CAPTION = "What a busy man!";
            const string ABORT_MESSAGE = "Guess you want to work a little longer, sorry for interrupting...";
            if (ManualClockActionPrompt("out", ABORT_MESSAGE, ABORT_CAPTION))
            {
                OnClockOut();
                return true;
            }
            return false;
        }

        private static bool ManualClockActionPrompt(string clockAction, string abortMessage, string abortCaption)
        {
            int declines = 0;
            bool hasClocked = false;
            string caption = "Clocked-" + clockAction + " yet?";
            string text = "Have you clocked-" + clockAction + " yet?";
            Utils.OpenWebBrowser(clockingSystem.GetWebLoginURL());
            while (!hasClocked)
            {
                hasClocked = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (!hasClocked) //Has he still not clocked in/out yet?
                {
                    declines++;
                    if (declines == MAX_DECLINES)
                    {
                        MessageBox.Show(abortMessage, abortCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Terminate()
        {
            running = false;
        }
    }
}
