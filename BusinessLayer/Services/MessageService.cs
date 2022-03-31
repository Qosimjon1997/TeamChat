using BusinessLayer.Hubs;
using BusinessLayer.IHubs;
using BusinessLayer.Interfaces;
using DataLayer.IRepositories;
using DataLayer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _message;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public MessageService(IRepository<Message> message, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _message = message;
            _hubContext = hubContext;
        }

        public async Task<bool> AddMessage(Message message)
        {
            if (message == null)
            {
                return false;
            }
            else
            {
                message.SentTime = DateTime.Now;
                await _message.CreateAsync(message);
                await _hubContext.Clients.All.SendTextMessage(message);
                return true;
            }
        }

        public async Task<IEnumerable<Message>> GetAllMessagesFromUser(Guid fromId, Guid toId)
        {
            var messages = await _message.GetAllAsync();
            var finalMessages = (from items in messages
                     where (items.FromMessage == toId || items.ToMessage == toId) && (items.FromMessage == fromId || items.ToMessage == fromId)
                     orderby items.SentTime
                     select items).ToList();
            return finalMessages;
        }
    }
}
