using System;

namespace DataLayer.Dtos.ApplicationUserDtos
{
    public class ApplicationUserReadDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FIO { get; set; }
    }
}
