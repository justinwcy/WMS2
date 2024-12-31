using Application.DTO.BaseDTO.Identity;

namespace Application.DTO.Request.Identity
{
    public class ChangePasswordStaffRequestDTO
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
