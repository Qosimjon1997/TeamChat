using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGroup
    {
        public Task<IEnumerable<Group>> GetAllGroups();
        public Task<Group> GetGroupById(Guid id);
        public Task<bool> AddGroup(Group group);
    }
}
