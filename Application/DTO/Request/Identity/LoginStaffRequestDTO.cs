using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Identity
{
    public class LoginStaffRequestDTO
    {
        [EmailAddress]
        [RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$", 
            ErrorMessage = "Your Email is invalid")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,}$", 
            ErrorMessage = "Your password is too weak!")]
        [MinLength(8), MaxLength(100)]
        public string Password { get; set; }
    }
}