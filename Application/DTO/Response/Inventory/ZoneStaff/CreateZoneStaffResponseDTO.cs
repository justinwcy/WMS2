using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateZoneStaffResponseDTO : ZoneStaffBaseDTO
    {
        public Guid Id { get; set; }
    }
}
