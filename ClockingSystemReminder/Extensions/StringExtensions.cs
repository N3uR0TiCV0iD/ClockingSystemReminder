using System;

namespace ClockingSystemReminder.Extensions
{
    public static class StringExtensions
    {
        public static string ExtractBetween(this string self, string key, char delim, bool includeDelim = false)
        {
            var substringStart = self.IndexOfAfter(key);
            var substringEnd = self.IndexOf(delim, substringStart);
            if (includeDelim)
            {
                substringEnd++;
            }
            return self.Substring(substringStart, substringEnd - substringStart);
        }

        public static int IndexOfAfter(this string self, string value)
        {
            return self.IndexOfAfter(value, 0);
        }

        public static int IndexOfAfter(this string self, string value, int startIndex)
        {
            var index = self.IndexOf(value, startIndex);
            if (index == -1)
            {
                return -1;
            }
            return index + value.Length;
        }

        public static int LastIndexOfAfter(this string self, string value)
        {
            return self.LastIndexOfAfter(value, self.Length - 1);
        }

        public static int LastIndexOfAfter(this string self, string value, int startIndex)
        {
            var index = self.LastIndexOf(value, startIndex);
            if (index == -1)
            {
                return -1;
            }
            return index + value.Length;
        }
    }
}
