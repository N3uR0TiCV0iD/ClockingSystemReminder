using System;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.ClockingSystems.Capitech;
using ClockingSystemReminder.ClockingSystems.Void;

namespace ClockingSystemReminder.ClockingSystems
{
    public static class ClockingSystemResolver
    {
        public static ClockingSystem Resolve(string systemName)
        {
            switch (systemName)
            {
                case "[None]": return new VoidClockingSystem();
                case "WinTid": return new WinTid.WinTid();
                case "MyCapitech": return new MyCapitech();
            }
            return null;
        }
    }
}
