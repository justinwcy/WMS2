using Domain.Entities;

namespace Application.DTO.BaseDTO
{
    public class ProductGroupProductBaseDTO : BaseDTO
    {
        public Guid ProductId { get; set; }

        public Guid ProductGroupId { get; set; }
    }
}
