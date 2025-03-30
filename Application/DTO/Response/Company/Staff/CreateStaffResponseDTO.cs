using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateStaffResponseDTO : StaffBaseDTO
    {
        public Guid Id { get; set; }
    }
}
