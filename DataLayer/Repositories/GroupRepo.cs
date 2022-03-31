using DataLayer.Data;
using DataLayer.IRepositories;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class GroupRepo : IRepository<Group>
    {
        private readonly ApplicationDbContext _context;

        public GroupRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(Group _object)
        {
            await _context.Groups.AddAsync(_object);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteAsync(Group _object)
        {
            _context.Groups.Remove(_object);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetByIdAsync(Guid id)
        {
            return await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Group _object)
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
