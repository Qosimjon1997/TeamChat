using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IMessageService
    {
        public Task<IEnumerable<Message>> GetAllMessagesFromUser(Guid fromId, Guid toId);
        public Task<bool> AddMessage(Message message);
    }
}
