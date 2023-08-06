using System;

namespace ClockingSystemReminder.Data
{
    public class PeriodInfo
    {
        public DateTime EndTime { get; }
        public DateTime StartTime { get; }

        public TimeSpan Duration => this.EndTime - this.StartTime;

        public PeriodInfo(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException($"The {nameof(startTime)} must be earlier than the {nameof(endTime)}.", nameof(startTime));
            }
            this.EndTime = endTime;
            this.StartTime = startTime;
        }

        public bool Contains(DateTime time)
        {
            return time >= this.StartTime && time <= this.EndTime;
        }
    }
}
