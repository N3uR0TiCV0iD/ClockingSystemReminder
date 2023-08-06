using System;
using System.Diagnostics;

namespace ClockingSystemReminder.Helpers
{
    public class OffsetStopwatch : Stopwatch
    {
        public TimeSpan Offset { get; set; }

        public OffsetStopwatch() { }
        public OffsetStopwatch(TimeSpan offset)
        {
            this.Offset = offset;
        }

        public new TimeSpan Elapsed
        {
            get
            {
                return base.Elapsed + this.Offset;
            }
            set
            {
                this.Offset = value - base.Elapsed;
            }
        }

        public new long ElapsedMilliseconds
        {
            get
            {
                return base.ElapsedMilliseconds + this.Offset.Milliseconds;
            }
            set
            {
                this.Offset = TimeSpan.FromMilliseconds(value - base.ElapsedMilliseconds);
            }
        }

        public new long ElapsedTicks
        {
            get
            {
                return base.ElapsedTicks + this.Offset.Ticks;
            }
            set
            {
                this.Offset = TimeSpan.FromTicks(value - base.ElapsedTicks);
            }
        }
    }
}
