using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ClockingSystemReminder
{
    public static class ValidationUtils
    {
        public static bool IsValidURL(string url, bool expectTrailingSlash = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            if (url.StartsWith(' ') || url.EndsWith(' '))
            {
                return false;
            }
            if (expectTrailingSlash && !url.EndsWith('/'))
            {
                return false;
            }
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uri) &&
                   (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidGUID(string guid)
        {
            const string regexPattern = @"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$";
            if (guid == null)
            {
                return false;
            }
            return Regex.IsMatch(guid, regexPattern, RegexOptions.IgnoreCase);
        }
    }
}
