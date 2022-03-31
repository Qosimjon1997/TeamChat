using DataLayer.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UsersOfGroups
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
