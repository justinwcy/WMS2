using System.ComponentModel.DataAnnotations;

namespace WebUI.Components.Models
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(36, ErrorMessage = "First name is too long", MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(36, ErrorMessage = "Last name is too long", MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(36, ErrorMessage = "Email is too long", MinimumLength = 2)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, ErrorMessage = "Phone number is too long", MinimumLength = 2)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(36, ErrorMessage = "Password is too long", MinimumLength = 2)]
        public string Password { get; set; } = string.Empty;
    }
}
