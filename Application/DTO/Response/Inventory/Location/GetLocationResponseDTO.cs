using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetLocationResponseDTO : LocationBaseDTO
    {
        public Guid Id { get; set; }
    }
}