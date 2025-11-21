using System.ComponentModel.DataAnnotations;

namespace NRGUserPortal.Models
{
    // simple login model
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
