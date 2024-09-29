using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateStaffNotificationRequestDTO : StaffNotificationBaseDTO
    {
        public Guid Id { get; set; }
    }
}
