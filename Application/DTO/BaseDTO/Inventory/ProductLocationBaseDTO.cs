namespace Application.DTO.BaseDTO
{
    public class ProductLocationBaseDTO : BaseDTO
    {
        public Guid LocationId { get; set; }
        public Guid ProductId { get; set; }
    }
}
