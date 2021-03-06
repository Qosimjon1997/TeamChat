using System;

namespace DataLayer.Dtos.NotificationDtos
{
    public class NotificationUpdateDto
    {
        public Guid FromMessage { get; set; }
        public string MessageText { get; set; }
        public DateTimeOffset SentTime { get; set; }
    }
}
