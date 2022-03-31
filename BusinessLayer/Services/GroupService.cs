using BusinessLayer.Hubs;
using BusinessLayer.IHubs;
using BusinessLayer.Interfaces;
using DataLayer.IRepositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GroupService : IGroup
    {
        private readonly IRepository<Group> _repository;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public GroupService(IRepository<Group> repository, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }
        public async Task<bool> AddGroup(Group group)
        {
            if (group == null)
            {
                return false;
            }
            else
            {
                await _repository.CreateAsync(group);
                await _hubContext.Clients.All.AllNotificationMessages();
                return true;
            }
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Group> GetGroupById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
