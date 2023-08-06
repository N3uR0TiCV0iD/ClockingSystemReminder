using System;
using System.Windows.Forms;
using ClockingSystemReminder.Extensions;
using ClockingSystemReminder.Helpers;
using Microsoft.Win32;

namespace ClockingSystemReminder
{
    internal static class Program
    {
        public const string REGISTRY_KEY_PATH = "Software\\ClockingSystemReminder";
        public static LocalEncrypter LocalEncrypter { get; private set; }

        [STAThread]
        private static void Main(string[] args)
        {
            SingleInstanceHelper.CheckAndRun(Startup, SingleInstanceAction.None);
        }

#if DEBUG
        private static void DEBUG_Startup()
        {
            var jira = TicketingSystems.TicketingSystemResolver.Load("Jira");
            var msTeams = CollaborationSystems.CollaborationSystemResolver.Load("MSTeams");
            Application.Run(new TimeRegistrationForm(msTeams, jira));
        }
#endif

        private static void Startup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Protects data against casual observation, not robust against a targeted attack
            LocalEncrypter = new LocalEncrypter("Digi7aLH0urgla55");

#if DEBUG
            DEBUG_Startup();
#endif

            using (var appRegistryKey = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH))
            {
                var today = DateTime.Today;
                var lastClockOut = appRegistryKey.GetDateTimeValue("LastClockOut");
                AutoStartupHelper.CheckAutoStartup(appRegistryKey, "ClockingSystemReminder");
                if (HasCompletedWorkday(today, lastClockOut, appRegistryKey))
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
