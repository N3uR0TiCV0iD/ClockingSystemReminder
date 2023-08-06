using System;

namespace ClockingSystemReminder.TicketingSystems.Jira
{
    public class JiraSettings
    {
        public string URL { get; set; }
        public string Email { get; set; }
        public string JiraToken { get; set; }
        public string TempoToken { get; set; }

        public bool IsValid()
        {
            return ValidationUtils.IsValidURL(this.URL, true) &&
                   ValidationUtils.IsValidEmail(this.Email) &&
                   !string.IsNullOrEmpty(this.JiraToken) &&
                   !string.IsNullOrEmpty(this.TempoToken);
        }
    }
}
