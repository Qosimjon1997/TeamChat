using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataLayer.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string FIO { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<UsersOfGroups> UsersOfGroups { get; set; }

    }
}
