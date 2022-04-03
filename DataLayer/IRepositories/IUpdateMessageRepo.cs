using DataLayer.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepositories
{
    public interface IUpdateMessageRepo<T> : IRepository<T>
    {
        public Task<bool> UpdateMessageWithList(List<Message> messages);
    }
}
