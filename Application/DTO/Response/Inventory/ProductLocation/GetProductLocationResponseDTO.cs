using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductLocationResponseDTO : ProductLocationBaseDTO
    {
        public Guid Id { get; set; }
    }
}