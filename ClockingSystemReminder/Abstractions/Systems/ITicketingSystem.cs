using System;
using System.Collections.Generic;
using ClockingSystemReminder.Data.Ticketing;

namespace ClockingSystemReminder.Abstractions.Systems
{
    public interface ITicketingSystem
    {
        void Init();
        bool LoadSettings();
        void OpenSettings();
        bool IsConfigured();

        IList<TicketInfo> GetFavoriteTickets();
        IList<TicketInfo> GetAssignedTickets();
        IList<TicketInfo> SearchTickets(string query);
        bool RegisterHours(DateTime workDate, TimeRegistration timeRegistration);
    }
}
