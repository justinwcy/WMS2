namespace Application.DTO.BaseDTO
{
    public class CourierBaseDTO : BaseDTO
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string Remark { get; set; }
    }
}
