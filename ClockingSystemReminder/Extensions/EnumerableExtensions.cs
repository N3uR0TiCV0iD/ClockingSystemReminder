using System;
using System.Collections.Generic;

namespace ClockingSystemReminder.Extensions
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> self, T item)
        {
            int index = 0;
            foreach (var currItem in self)
            {
                if (Equals(currItem, item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static int LastIndexOf<T>(this IEnumerable<T> self, T item)
        {
            int index = 0;
            int foundIndex = -1;
            foreach (var currItem in self)
            {
                if (Equals(currItem, item))
                {
                    foundIndex = index;
                }
                index++;
            }
            return foundIndex;
        }
    }
}
