using System;
using System.Globalization;
using ClockingSystemReminder.Helpers;
using Microsoft.Win32;

namespace ClockingSystemReminder.Extensions
{
    public static class RegistryKeyExtensions
    {
        public static DateTime GetDateTimeValue(this RegistryKey registryKey, string valueName)
        {
            var dateValue = registryKey.GetValue(valueName);
            if (dateValue != null)
            {
                return DateTime.ParseExact(dateValue.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            return DateTime.MinValue;
        }

        public static void SetDateTimeValue(this RegistryKey registryKey, string valueName, DateTime value)
        {
            registryKey.SetValue(valueName, value.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
        }

        public static TimeSpan GetTimeSpanValue(this RegistryKey registryKey, string valueName)
        {
            var timeSpanValue = registryKey.GetValue(valueName);
            if (timeSpanValue != null)
            {
                return TimeSpan.Parse(timeSpanValue.ToString());
            }
            return TimeSpan.Zero;
        }

        public static void SetTimeSpanValue(this RegistryKey registryKey, string valueName, TimeSpan value)
        {
            registryKey.SetValue(valueName, value.ToString());
        }

        public static string GetEncryptedValue(this RegistryKey registryKey, string valueName, LocalEncrypter localEncrypter)
        {
            var encryptedData = registryKey.GetValue(valueName);
            if (encryptedData == null)
            {
                return null;
            }
            return localEncrypter.DecryptText((byte[])encryptedData);
        }

        public static void SetEncryptedValue(this RegistryKey registryKey, string valueName, string value, LocalEncrypter localEncrypter)
        {
            var encryptedData = localEncrypter.EncryptText(value);
            registryKey.SetValue(valueName, encryptedData, RegistryValueKind.Binary);
        }
    }
}
