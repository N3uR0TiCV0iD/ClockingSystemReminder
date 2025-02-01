using System;
using ClockingSystemReminder.ClockingSystems.WinTid;

namespace DutyRegistrationReminder.ClockingSystems
{
    public class WinTidExtension : WinTid, IDutyClockingSystem
    {
        public bool AutoRegisterDutyWeek(DateTime today, int weekNumber)
        {
            throw new NotImplementedException();
        }

        public void OpenDutyWeekRegistration()
        {
            throw new NotImplementedException();
        }
    }
}
