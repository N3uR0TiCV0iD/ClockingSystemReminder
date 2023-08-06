using System;

namespace ClockingSystemReminder.Data.Collaboration
{
    public class UserInfo
    {
        public string UserID { get; }
        public string ChatID { get; }
        public string FullName { get; }

        public UserInfo(string userID, string chatID, string fullName)
        {
            UserID = userID;
            ChatID = chatID;
            FullName = fullName;
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }
    }
}
