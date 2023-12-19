using System.ComponentModel.DataAnnotations;

namespace ECommerce_API.Models.Authentication.LogIn
{
    public class LogInUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
