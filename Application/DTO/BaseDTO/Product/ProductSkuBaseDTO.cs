namespace Application.DTO.BaseDTO
{
    public class ProductSkuBaseDTO : BaseDTO
    {
        public Guid ProductId { get; set; }
        public string Sku { get; set; }
    }
}
