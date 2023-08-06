using System;
using ClockingSystemReminder.Data;

namespace ClockingSystemReminder.ClockingSystems.Capitech
{
    public class MyCapitechCredentials : BasicCredentials
    {
        public int ClientID { get; }

        public MyCapitechCredentials(int clientID, string username, string password) : base(username, password)
        {
            this.ClientID = clientID;
        }
    }
}
