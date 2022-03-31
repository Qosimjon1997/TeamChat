using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FIO is required")]
        public string FIO { get; set; }

    }
}
