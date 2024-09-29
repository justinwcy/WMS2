using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetZoneResponseDTO : ZoneBaseDTO
    {
        public Guid Id { get; set; }
    }
}