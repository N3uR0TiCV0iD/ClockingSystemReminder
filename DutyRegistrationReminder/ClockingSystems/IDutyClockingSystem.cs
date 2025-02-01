using System;

namespace DutyRegistrationReminder.ClockingSystems
{
    public interface IDutyClockingSystem
    {
        bool AutoRegisterDutyWeek(DateTime today, int weekNumber);
        void OpenDutyWeekRegistration();
    }
}
