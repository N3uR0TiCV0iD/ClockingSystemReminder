﻿using System;
using ClockingSystemReminder.Abstractions.Systems;
using ClockingSystemReminder.Data;
using Microsoft.Win32;

namespace ClockingSystemReminder.ClockingSystems.Void
{
    public class VoidClockingSystem : ClockingSystem
    {
        public override string FriendlySystemName => "[None]";

        public override void LoadSettings(RegistryKey systemRegistryKey)
        {
            throw new NotImplementedException();
        }

        public override bool ClockIn()
        {
            return true;
        }

        public override bool ClockOut()
        {
            return true;
        }

        public override string GetWebLoginURL()
        {
            return null;
        }

        public override bool Login(BasicCredentials credentials)
        {
            return true;
        }

        public override void OpenSettings()
        {
        }

        public override bool LogOut()
        {
            return true;
        }
    }
}
