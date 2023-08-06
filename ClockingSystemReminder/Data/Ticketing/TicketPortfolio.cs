using System;
using System.Collections.Generic;

namespace ClockingSystemReminder.Data.Ticketing
{
    public class TicketPortfolio
    {
        public const int MAX_RECENT_TICKETS = 8;

        public IList<TicketInfo> RecentTickets { get; }
        public IList<TicketInfo> FavoriteTickets { get; }
        public IList<TicketInfo> AssignedTickets { get; }

        public TicketPortfolio(IList<TicketInfo> recentTickets, IList<TicketInfo> favoriteTickets, IList<TicketInfo> assignedTickets)
        {
            this.RecentTickets = recentTickets;
            this.FavoriteTickets = favoriteTickets;
            this.AssignedTickets = assignedTickets;
        }

        public bool IsFavoriteTicket(TicketInfo ticket)
        {
            return this.FavoriteTickets.Contains(ticket);
        }
    }
}
