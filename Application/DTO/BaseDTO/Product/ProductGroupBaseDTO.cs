using Domain.Entities;

namespace Application.DTO.BaseDTO
{
    public class ProductGroupBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Guid>? ProductIds { get; set; }

        public List<Guid>? PhotoIds { get; set; }
    }
}
