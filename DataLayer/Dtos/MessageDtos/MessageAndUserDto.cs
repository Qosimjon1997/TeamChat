using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos.MessageDtos
{
    public class MessageAndUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FIO { get; set; }
        public bool isRead { get; set; }
    }
}
