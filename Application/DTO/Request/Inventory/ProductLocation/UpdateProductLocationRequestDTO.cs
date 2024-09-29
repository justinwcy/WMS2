using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductLocationRequestDTO : ProductLocationBaseDTO
    {
        public Guid Id { get; set; }
    }
}
