using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateZoneResponseDTO : ZoneBaseDTO
    {
        public Guid Id { get; set; }
    }
}
