using System;

namespace DynamicRulesEngine.Entities
{
    public class Event
    {
        public Event()
        {
            this.Timestamp = String.Empty;
            this.Status = String.Empty;
            this.Count = String.Empty;
            this.Processed = String.Empty;
        }

        public string Timestamp { get; set; }

        public string Status { get; set; }

        public string Count { get; set; }

        public string Processed { get; set; }
    }
}
