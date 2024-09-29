using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetZoneStaffResponseDTO : ZoneStaffBaseDTO
    {
        public Guid Id { get; set; }
    }
}