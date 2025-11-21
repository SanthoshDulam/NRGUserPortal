using System.ComponentModel.DataAnnotations;

namespace NRGUserPortal.Models
{
    // ViewModel for register page - validation here
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password needs minimum 6 chars")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
