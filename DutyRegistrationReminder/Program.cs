using System;
using System.Windows.Forms;
using ClockingSystemReminder;
using ClockingSystemReminder.ClockingSystems.WinTid;
using ClockingSystemReminder.Data;
using ClockingSystemReminder.Extensions;
using ClockingSystemReminder.Helpers;
using DutyRegistrationReminder.ClockingSystems;
using Microsoft.Win32;

namespace DutyRegistrationReminder
{
    internal static class Program
    {
        public const string APP_NAME = "DutyRegistrationReminder";
        public const string REGISTRY_KEY_PATH = $"Software\\{APP_NAME}";
        public static LocalEncrypter LocalEncrypter { get; }

        static Program()
        {
            //Protects data against casual observation, not robust against a targeted attack
            LocalEncrypter = new LocalEncrypter("Cyb3rViGiL4nt3");
        }

        [STAThread]
        private static void Main(string[] args)
        {
            SingleInstanceHelper.CheckAndRun(Startup, SingleInstanceAction.None);
        }

        private static void Startup()
        {
            ApplicationConfiguration.Initialize();
            using (var appRegistryKey = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH))
            {
                AutoStartupHelper.CheckAutoStartup(appRegistryKey, APP_NAME);

                var rotationSchedule = GetRotationSchedule(appRegistryKey);
                if (rotationSchedule == null)
                {
                    //We did not have the config and the user closed the prompt
                    return;
                }

                var today = DateTime.Today;
                var weekNumber = Utils.GetWeekNumber(today);
                var lastRegistrationWeek = (int)appRegistryKey.GetValue("LastRegistrationWeek", -1)!;
                if (weekNumber == lastRegistrationWeek)
                {
                    //TODO: Add check above for "auto run" flag
                    return;
                }

                var clockingSystem = new WinTidExtension();
                Register24_7DutyProcess(today, weekNumber, clockingSystem);
            }
        }

        private static WeekRotationSchedule? GetRotationSchedule(RegistryKey appRegistryKey)
        {
            var startDate = appRegistryKey.GetDateTimeValue("DutyStartDate");
            if (startDate != DateTime.MinValue)
            {
                return OpenRotationScheduleDialog();
            }

            var rotationWeeks = (int)appRegistryKey.GetValue("DutyRotationWeeks")!;
            return new WeekRotationSchedule(startDate, rotationWeeks);
        }

        private static WeekRotationSchedule? OpenRotationScheduleDialog()
        {
            using (var rotationScheduleDialog = new WeekRotationScheduleDialog(null))
            {
                if (rotationScheduleDialog.ShowDialog() == DialogResult.OK)
                {
                    return rotationScheduleDialog.WeekRotationSchedule;
                }
                return null;
            }
        }

        private static bool Register24_7DutyProcess(DateTime today, int weekNumber, IDutyClockingSystem clockingSystem)
        {
            if (MessageBox.Show("Looks like it's time to register your 24/7 week!\n\nWould you like to automatically register it?",
                                "Auto register 24/7 week?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return clockingSystem.AutoRegisterDutyWeek(today, weekNumber);
            }
            clockingSystem.OpenDutyWeekRegistration();
            return true;
        }
    }
}
