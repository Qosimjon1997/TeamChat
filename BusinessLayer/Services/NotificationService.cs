using BusinessLayer.Hubs;
using BusinessLayer.IHubs;
using BusinessLayer.Interfaces;
using DataLayer.IRepositories;
using DataLayer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notification;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public NotificationService(IRepository<Notification> notification, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _notification = notification;
            _hubContext = hubContext;
        }

        public async Task<bool> AddNotification(Notification notification)
        {
            if (notification == null)
            {
                return false;
            }
            else
            {
                await _notification.CreateAsync(notification);
                await _hubContext.Clients.All.AllNotificationMessages();
                return true;
            }
        }
        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _notification.GetAllAsync();
        }

        public async Task<Notification> GetNotificationById(Guid id)
        {
            return await _notification.GetByIdAsync(id);
        }
    }
}
