using System;
using ClockingSystemReminder.Data;
using ClockingSystemReminder.Extensions;
using Microsoft.Win32;

namespace ClockingSystemReminder.Abstractions.Systems
{
    public abstract class ClockingSystem
    {
        public virtual string FriendlySystemName => this.SystemName;

        private string SystemName => this.GetType().Name;

        public RegistryKey OpenRegistryKey()
        {
            return Utils.OpenSystemRegistryKey(this.SystemName);
        }

        public virtual BasicCredentials GetStoredCredentials(RegistryKey systemRegistryKey)
        {
            if (TryGetStoredCredentials(systemRegistryKey, out string username, out string password))
            {
                return new BasicCredentials(username, password);
            }
            return null;
        }

        protected bool TryGetStoredCredentials(RegistryKey systemRegistryKey, out string username, out string password)
        {
            username = systemRegistryKey.GetEncryptedValue("Username", Program.LocalEncrypter);
            if (username == null)
            {
                password = null;
                return false;
            }

            password = systemRegistryKey.GetEncryptedValue("Password", Program.LocalEncrypter);
            if (password == null)
            {
                username = null;
                return false;
            }

            return true;
        }

        public virtual AbstractLoginDialog CreateLoginDialog()
        {
            return new LoginDialog(this.SystemName);
        }

        public virtual BasicCredentials GetCredentialsFromLoginDialog(AbstractLoginDialog loginDialog)
        {
            return new BasicCredentials(loginDialog.Username, loginDialog.Password);
        }

        public virtual void SaveCredentials(BasicCredentials credentials, RegistryKey systemRegistryKey)
        {
            systemRegistryKey.SetEncryptedValue("Username", credentials.Username, Program.LocalEncrypter);
            systemRegistryKey.SetEncryptedValue("Password", credentials.Password, Program.LocalEncrypter);
        }

        public virtual void DropCredentials(RegistryKey systemRegistryKey)
        {
            systemRegistryKey.DeleteValue("Username");
            systemRegistryKey.DeleteValue("Password");
        }

        public abstract string GetWebLoginURL();
        public abstract TimeSpan GetTimeWorked(DateTime day);
        public abstract bool ClockIn();
        public abstract bool ClockOut();
        public abstract bool Login(BasicCredentials credentials);
        public abstract bool LogOut();
        public virtual bool OnPostClockIn() => true;
        public virtual bool OnPostClockOut() => true;
    }
}
