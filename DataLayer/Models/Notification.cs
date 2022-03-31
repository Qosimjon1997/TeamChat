using System;

namespace DataLayer.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid FromMessage { get; set; }
        public string MessageText { get; set; }
        public DateTimeOffset SentTime { get; set; }
    }
}
