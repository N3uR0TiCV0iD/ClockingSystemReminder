using System;
using Newtonsoft.Json.Linq;

#pragma warning disable SA1204 // Static elements should appear before instance elements
namespace ClockingSystemReminder.Data.Ticketing
{
    public class TicketInfo
    {
        public int ID { get; }
        public string Key { get; }
        public string Title { get; }

        public static TicketInfo FromJson(string json)
        {
            var ticketData = JObject.Parse(json);
            var result = new TicketInfo(ticketData.Value<int>(nameof(ID)),
                                        ticketData.Value<string>(nameof(Key)),
                                        ticketData.Value<string>(nameof(Title)));
            return result;
        }

        public TicketInfo(int id, string key, string title)
        {
            this.ID = id;
            this.Key = key;
            this.Title = title;
        }

        public string ToStringShort(int maxTitleChars)
        {
            var shortTitle = Utils.ShortenString(this.Title, maxTitleChars);
            return $"{Key} | {shortTitle}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(this, (TicketInfo)obj);
        }

        public bool Equals(TicketInfo other)
        {
            return Equals(this, other);
        }

        public static bool Equals(TicketInfo left, TicketInfo right)
        {
            return left.ID == right.ID &&
                   left.Key == right.Key &&
                   left.Title == right.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ID, this.Key, this.Title);
        }

        public override string ToString() => $"{Key} | {Title}";
    }
}
#pragma warning restore SA1204 // Static elements should appear before instance elements
