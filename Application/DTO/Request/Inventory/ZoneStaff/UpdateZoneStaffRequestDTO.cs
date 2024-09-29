using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateZoneStaffRequestDTO : ZoneStaffBaseDTO
    {
        public Guid Id { get; set; }
    }
}
