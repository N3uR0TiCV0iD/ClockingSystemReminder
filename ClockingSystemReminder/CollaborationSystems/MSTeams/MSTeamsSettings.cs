using System;

namespace ClockingSystemReminder.CollaborationSystems.MSTeams
{
    public class MSTeamsSettings
    {
        public string TenantID { get; set; }

        public bool IsValid()
        {
            return ValidationUtils.IsValidGUID(this.TenantID);
        }
    }
}
