using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ClockingSystemReminder.TicketingSystems.Jira
{
    public class TempoPayload
    {
        [JsonProperty("attributes")]
        public IEnumerable<object> Attributes => Enumerable.Empty<object>();

        [JsonProperty("authorAccountId")]
        public string AuthorAccountId { get; }

        [JsonProperty("billableSeconds")]
        public int BillableSeconds { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("issueId")]
        public int IssueId { get; }

        [JsonProperty("remainingEstimateSeconds")]
        public int RemainingEstimateSeconds => 0;

        [JsonProperty("startDate")]
        public string StartDate { get; }

        [JsonProperty("startTime")]
        public string StartTime => "08:00:00";

        [JsonProperty("timeSpentSeconds")]
        public int TimeSpentSeconds { get; }

        public TempoPayload(string authorAccountId, int issueId, DateTime workDate, int secondsWorked, string description)
        {
            this.AuthorAccountId = authorAccountId;
            this.BillableSeconds = secondsWorked;
            this.Description = description;
            this.IssueId = issueId;
            this.StartDate = workDate.ToString("yyyy-MM-dd");
            this.TimeSpentSeconds = secondsWorked;
        }
    }
}
