using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Auth
{
    public class ResetPasswordModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
