using System;
using System.Collections.Generic;

namespace ClockingSystemReminder.Data.Collaboration
{
    public class CallRecord
    {
        readonly HashSet<UserInfo> participants;

        public string ChatID { get; }
        public PeriodInfo Period { get; }
        public IEnumerable<UserInfo> Participants => participants;

        public TimeSpan Duration => Period.Duration;

        public bool HasParticipant(UserInfo participant)
        {
            return participants.Contains(participant);
        }

        public CallRecord(string chatID, PeriodInfo period, HashSet<UserInfo> participants)
        {
            ChatID = chatID;
            Period = period;
            this.participants = participants;
        }
    }
}
