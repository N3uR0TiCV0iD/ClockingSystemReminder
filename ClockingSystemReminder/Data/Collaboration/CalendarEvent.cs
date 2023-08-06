using System;

namespace ClockingSystemReminder.Data.Collaboration
{
    public class CalendarEvent
    {
        public string Title { get; }
        public string ChatID { get; }
        public PeriodInfo Period { get; }

        public TimeSpan Duration => Period.Duration;

        public CalendarEvent(string title, string chatID, PeriodInfo period)
        {
            Title = title;
            ChatID = chatID;
            Period = period;
        }
    }
}
