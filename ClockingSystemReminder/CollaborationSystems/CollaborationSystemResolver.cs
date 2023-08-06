using System;
using ClockingSystemReminder.Abstractions.Systems;

namespace ClockingSystemReminder.CollaborationSystems
{
    public static class CollaborationSystemResolver
    {
        public static ICollaborationSystem Load(string systemName)
        {
            switch (systemName)
            {
                case "MSTeams":
                    var msTeams = new MSTeams.MSTeams();
                    msTeams.LoadSettings();
                    return msTeams;
                //break;
            }
            return null;
        }
    }
}
