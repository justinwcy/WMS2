using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductRackResponseDTO : ProductRackBaseDTO
    {
        public Guid Id { get; set; }
    }
}