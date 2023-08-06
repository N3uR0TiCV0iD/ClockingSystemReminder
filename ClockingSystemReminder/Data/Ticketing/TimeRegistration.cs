using System;

namespace ClockingSystemReminder.Data.Ticketing
{
    public class TimeRegistration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public TicketInfo Ticket { get; set; }
        public string Description { get; set; }

        public TimeSpan Duration => new TimeSpan(Hours, Minutes, 0);

        public TimeRegistration()
        {
        }

        public TimeRegistration(TimeSpan duration, string description)
        {
            this.Hours = duration.Hours;
            this.Minutes = duration.Minutes;
            this.Description = description;
        }
    }
}
