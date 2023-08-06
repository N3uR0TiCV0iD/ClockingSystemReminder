using System;
using System.Collections.Generic;
using ClockingSystemReminder.Data.Collaboration;

namespace ClockingSystemReminder.Abstractions.Systems
{
    public interface ICollaborationSystem
    {
        UserInfo MyUser { get; }

        void Init();
        bool LoadSettings();
        void OpenSettings();
        bool IsConfigured();

        IEnumerable<CallRecord> GetCallHistory();
        IEnumerable<ChatRecord> GetChatHistory(string chatID);
        IList<CalendarEvent> GetCalendarEvents(DateTime utcFrom, DateTime utcTo);
        CallRecord GetCallForCalendarEvent(CalendarEvent calendarEvent);
    }
}
