using DataLayer.Data;
using DataLayer.IRepositories;
using DataLayer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class MessageRepo : IRepository<Message>, IUpdateMessageRepo<Message>
    {
        private readonly ApplicationDbContext _context;

        public MessageRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Message _object)
        {
            await _context.Messages.AddAsync(_object);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteAsync(Message _object)
        {
            _context.Messages.Remove(_object);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> GetByIdAsync(Guid id)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Message _object)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMessageWithList(List<Message> messages)
        {
            foreach(var item in messages)
            {
                item.isRead = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
