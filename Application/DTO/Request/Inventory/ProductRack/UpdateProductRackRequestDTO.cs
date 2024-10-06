using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductRackRequestDTO : ProductRackBaseDTO
    {
        public Guid Id { get; set; }
    }
}
