namespace Application.DTO.BaseDTO
{
    public class ShopBaseDTO : BaseDTO
    {
        public required string Name { get; set; }

        public required string Platform { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public List<Guid> ProductIds { get; set; }
    }
}
