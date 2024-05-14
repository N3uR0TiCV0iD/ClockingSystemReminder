using System;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.ClockingSystems.Capitech;
using ClockingSystemReminder.ClockingSystems.Void;

namespace ClockingSystemReminder.ClockingSystems
{
    public static class ClockingSystemResolver
    {
        public static ClockingSystem Load(string systemName)
        {
            switch (systemName)
            {
                case "[None]": return new VoidClockingSystem();
                case "WinTid":
                    var winTid = new WinTid.WinTid();
                    winTid.LoadSettings();
                    return winTid;
                case "MyCapitech": return new MyCapitech();
            }
            return null;
        }
    }
}
