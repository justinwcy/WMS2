using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Identity
{
    public class CreateStaffAccountRequestDTO : LoginStaffRequestDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
