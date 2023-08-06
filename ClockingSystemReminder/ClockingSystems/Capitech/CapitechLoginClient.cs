using System;
namespace ClockingSystemReminder.ClockingSystems.Capitech
{
    public class CapitechLoginClient
    {
        public int ID { get; }
        public string Name { get; }

        public CapitechLoginClient(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
