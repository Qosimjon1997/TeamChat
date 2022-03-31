using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INotificationService
    {
        public Task<IEnumerable<Notification>> GetAllNotifications();
        public Task<Notification> GetNotificationById(Guid id);
        public Task<bool>  AddNotification(Notification notification);
    }
}
