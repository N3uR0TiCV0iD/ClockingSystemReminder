using System;
using System.Windows.Forms;
using ClockingSystemReminder.Extensions;
using ClockingSystemReminder.Helpers;
using Microsoft.Win32;

namespace ClockingSystemReminder
{
    internal static class Program
    {
        public const string APP_NAME = "ClockingSystemReminder";
        public const string REGISTRY_KEY_PATH = $"Software\\{APP_NAME}";
        public static LocalEncrypter LocalEncrypter { get; private set; }

        [STAThread]
        private static void Main(string[] args)
        {
            SingleInstanceHelper.CheckAndRun(Startup, SingleInstanceAction.None);
        }

#if DEBUG
        private static void DEBUG_Startup()
        {
            var ticketingSystem = TicketingSystems.TicketingSystemResolver.Load("Jira");
            var collaborationSystem = CollaborationSystems.CollaborationSystemResolver.Load("MSTeams");

            var registerDate = new DateTime(2025, 1, 1);
            var workTime = new TimeSpan(7, 30, 0).Add(TimeSpan.FromMinutes(30));
            Application.Run(new TimeRegistrationForm(registerDate, workTime, collaborationSystem, ticketingSystem));
        }
#endif

        private static void Startup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Protects data against casual observation, not robust against a targeted attack
            LocalEncrypter = new LocalEncrypter("Digi7aLH0urgla55");

            using (var appRegistryKey = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH))
            {
                AutoStartupHelper.CheckAutoStartup(appRegistryKey, APP_NAME);

                var today = DateTime.Today;
                var lastClockOut = appRegistryKey.GetDateTimeValue("LastClockOut");
                if (HasCompletedWorkday(today, lastClockOut, appRegistryKey) && !ExtraWorkCheck())
                {
                    return;
                }

                var lastClockIn = appRegistryKey.GetDateTimeValue("LastClockIn");
                var hasClockedInToday = lastClockIn.Date == today;
                var workingToday = hasClockedInToday || !Utils.IsWeekEnd(today) || WeekendWorkCheck();
                if (!workingToday)
                {
                    return;
                }

                ClockingManager.Init(appRegistryKey);

                var isClockedIn = hasClockedInToday && lastClockIn > lastClockOut;
                if (!isClockedIn && ClockingManager.ClockInProcess())
                {
                    lastClockIn = DateTime.Now;
                    isClockedIn = true;
                }
                if (isClockedIn)
                {
                    ClockingManager.StartService(appRegistryKey, lastClockIn);
                }
            }
        }

        private static bool HasCompletedWorkday(DateTime today, DateTime lastClockOut, RegistryKey appRegistryKey)
        {
            if (lastClockOut.Date != today) //Have we ClockedOut at all today?
            {
                return false; //No we haven't...
            }
            TimeSpan timeWorked = appRegistryKey.GetTimeSpanValue("TimeWorked");
            return timeWorked.TotalHours >= ClockingManager.WORK_HOURS;
        }

        private static bool ExtraWorkCheck()
        {
            if (MessageBox.Show("Looks like you have already completed your work for the day...\nAre you planning to keep working today?",
                                "Extra Work Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Ah, a missclick? That's OK :)", "Have a nice day!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private static bool WeekendWorkCheck()
        {
            if (MessageBox.Show("Looks like it's a weekend today...\nAre you planning on working today?",
                                "Weekend Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                MessageBox.Show("Sorry for bothering you, have a nice weekend! :)", "Have a nice weekend!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}
