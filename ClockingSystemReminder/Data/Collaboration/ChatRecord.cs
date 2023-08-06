using System;

namespace ClockingSystemReminder.Data.Collaboration
{
    public class ChatRecord
    {
        public string From { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; }

        public ChatRecord(string from, string text, DateTime sendTime)
        {
            From = from;
            Text = text;
            SendTime = sendTime;
        }
    }
}
