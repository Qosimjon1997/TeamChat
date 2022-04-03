using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos.ApplicationUserDtos
{
    public class ForUsersMessageDto
    {
        public Guid fromId { get; set; }
        public Guid toId { get; set; }
    }
}
