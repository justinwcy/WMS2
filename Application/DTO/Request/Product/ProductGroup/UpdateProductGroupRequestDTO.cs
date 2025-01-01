using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductGroupRequestDTO : ProductGroupBaseDTO
    {
        public Guid Id { get; set; }
    }
}
