using DataLayer.Data;
using DataLayer.IRepositories;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class NotificationRepo : IRepository<Notification>
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Notification _object)
        {
            await _context.Notifications.AddAsync(_object);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteAsync(Notification _object)
        {
            _context.Notifications.Remove(_object);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(Guid id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Notification _object)
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
