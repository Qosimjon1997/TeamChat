using System;

namespace DataLayer.Dtos.MessageDtos
{
    public class MessageReadDto
    {
        public Guid Id { get; set; }
        public Guid FromMessage { get; set; }
        public Guid ToMessage { get; set; }
        public bool isFile { get; set; }
        public string MessageText { get; set; }
        public string FilePath { get; set; }
        public bool isRead { get; set; }
        public DateTimeOffset SentTime { get; set; }
    }
}
