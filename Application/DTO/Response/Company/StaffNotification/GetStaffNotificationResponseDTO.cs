using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetStaffNotificationResponseDTO : StaffNotificationBaseDTO
    {
        public Guid Id { get; set; }
    }
}
