using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClockingSystemReminder
{
    public static class Utils
    {
        public static DateTime UtcToday => DateTime.UtcNow.Date;
        public static DateTime TodayAsUtc => DateTime.Today.ToUniversalTime();

        public static DateTime ParseISODate(string dateString)
        {
            return DateTime.ParseExact(dateString, "O", CultureInfo.InvariantCulture);
        }

        public static int RoundToClosestMultiple(int value, int multiple)
        {
            var prevMultiple = (value / multiple) * multiple; //NOTE: int division, ie: Math.Floor is happening there!
            var nextMultiple = prevMultiple + multiple;
            var midPoint = (prevMultiple + nextMultiple) / 2F;
            return value > midPoint ? nextMultiple : prevMultiple;
        }

        public static RegistryKey OpenSystemRegistryKey(string systemName)
        {
            return Registry.CurrentUser.CreateSubKey("Software\\ClockingSystemReminder\\" + systemName);
        }

        public static void OpenWebBrowser(string url)
        {
            var uri = new Uri(url);
            if (uri.IsFile) //Just a security check :)
            {
                throw new ArgumentException("A file based URI is not allowed.");
            }
            var startInfo = new ProcessStartInfo(url)
            {
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }

        public static string ShortenString(string text, int maxChars)
        {
            if (text.Length <= maxChars)
            {
                return text;
            }
            return text.Substring(0, maxChars) + "...";
        }

        public static int GetWeekNumber(DateTime date)
        {
            var firstDayOfYear = new DateTime(date.Year, 1, 1);
            int dayOffset = DayOfWeekToIndex(firstDayOfYear.DayOfWeek);
            int dayOfYearIndex = date.DayOfYear - 1;
            return (dayOfYearIndex + dayOffset) / 7;
        }

        public static int DayOfWeekToIndex(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Sunday) //Edge case, since Sunday = 0
            {
                return 7;
            }
            return (int)dayOfWeek - 1;
        }

        public static DateTime GetLastDayInMonth(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, daysInMonth);
        }

        public static bool IsWeekEnd(DateTime date)
        {
            return IsWeekEnd(date.DayOfWeek);
        }

        public static bool IsWeekEnd(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

        public static TimeSpan SumTimeSpans(IEnumerable<TimeSpan> timeSpans)
        {
            var totalTicks = timeSpans.Sum(t => t.Ticks);
            var totalTimeSpan = new TimeSpan(totalTicks);
            return totalTimeSpan;
        }

        public static bool ShowErrorRetryMessage(Exception exception)
        {
            var message = $"An unexpected error has occured!\n\n{exception.GetType().Name}: {exception.Message}";
            return ShowRetryMessage(message, "Unexpected error");
        }

        public static bool ShowRetryMessage(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry;
        }

        //Useful for strings that are not "format compliant" :) [ie: have "open" '{' & '}']
        public static string StringFormat(string format, params object[] args)
        {
            var replacementMap = new Dictionary<string, string>();
            for (int index = 0; index < args.Length; index++)
            {
                replacementMap.Add($"{{{index}}}", args[index].ToString());
            }

            const string pattern = "{\\d+}";
            var result = Regex.Replace(format, pattern, match =>
            {
                if (replacementMap.TryGetValue(match.Value, out string replacement))
                {
                    return replacement;
                }
                return match.Value;
            });
            return result;
        }
    }
}
