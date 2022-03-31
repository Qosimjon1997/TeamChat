using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.IRepositories
{
    public interface IRepository<T>
    {
        public Task<bool> CreateAsync(T _object);
        public Task<bool> UpdateAsync(T _object);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(Guid id);
        public bool DeleteAsync(T _object);    


    }
}
