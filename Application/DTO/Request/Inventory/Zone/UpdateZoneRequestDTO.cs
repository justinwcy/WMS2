using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateZoneRequestDTO : ZoneBaseDTO
    {
        public Guid Id { get; set; }
    }
}
