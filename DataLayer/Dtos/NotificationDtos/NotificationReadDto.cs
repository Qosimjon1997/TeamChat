using System;

namespace DataLayer.Dtos.NotificationDtos
{
    public class NotificationReadDto
    {
        public Guid Id { get; set; }
        public Guid FromMessage { get; set; }
        public string MessageText { get; set; }
        public DateTimeOffset SentTime { get; set; }
    }
}
