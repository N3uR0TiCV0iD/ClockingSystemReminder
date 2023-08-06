using System;
using ClockingSystemReminder.Abstractions.Systems;

namespace ClockingSystemReminder.TicketingSystems
{
    public static class TicketingSystemResolver
    {
        public static ITicketingSystem Load(string systemName)
        {
            switch (systemName)
            {
                case "Jira":
                    var jira = new Jira.Jira();
                    jira.LoadSettings();
                    return jira;
                //break;
            }
            return null;
        }
    }
}
