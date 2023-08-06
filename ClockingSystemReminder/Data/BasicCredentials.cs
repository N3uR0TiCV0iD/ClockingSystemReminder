using System;

namespace ClockingSystemReminder.Data
{
    public class BasicCredentials
    {
        public string Username { get; }
        public string Password { get; }

        public BasicCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
