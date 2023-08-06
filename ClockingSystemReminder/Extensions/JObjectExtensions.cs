using System;
using Newtonsoft.Json.Linq;

namespace ClockingSystemReminder.Extensions
{
    public static class JObjectExtensions
    {
        public static DateTime ValueAsUTCDateTime(this JObject self, string key)
        {
            return self.Value<DateTime>(key).ToUniversalTime();
        }

        public static DateTime? ValueAsNullableUTCDateTime(this JObject self, string key)
        {
            return self.Value<DateTime?>(key)?.ToUniversalTime();
        }
    }
}
