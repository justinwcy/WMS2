using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class CreateStaffRequestDTO : StaffBaseDTO
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 
    }
}
