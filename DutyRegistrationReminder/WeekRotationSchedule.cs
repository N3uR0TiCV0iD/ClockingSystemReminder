using System;

namespace ClockingSystemReminder.Data
{
    public class WeekRotationSchedule
    {
        public DateTime StartDate { get; }
        public int RotationWeeks { get; set; }

        public WeekRotationSchedule(DateTime startDate, int rotationWeeks)
        {
            this.StartDate = FloorToMonday(startDate);
            this.RotationWeeks = rotationWeeks;
        }

        private DateTime FloorToMonday(DateTime date)
        {
            var dayIndex = Utils.DayOfWeekToIndex(date.DayOfWeek);
            return date.AddDays(-dayIndex).Date;
        }

        public bool IsDateInRotation(DateTime date)
        {
            var timeSpan = date.Date - this.StartDate;
            var weeksDifference = (int)timeSpan.TotalDays / 7;
            var weeksSinceRotation = weeksDifference % this.RotationWeeks;
            return weeksSinceRotation == 0;
        }

        public DateTime GetNextRotationDate(DateTime referenceDate)
        {
            var timeSpan = FloorToMonday(referenceDate) - this.StartDate;
            var totalDays = (int)timeSpan.TotalDays;
            var weeksDifference = totalDays / 7;

            var cyclesPassed = weeksDifference / this.RotationWeeks;
            var nextCycleStartWeek = (cyclesPassed + 1) * this.RotationWeeks;

            var daysUntilNextRotation = (nextCycleStartWeek * 7) - totalDays;
            return referenceDate.AddDays(daysUntilNextRotation);
        }
    }
}
