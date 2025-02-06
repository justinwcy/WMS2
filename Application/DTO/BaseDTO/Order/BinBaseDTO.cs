namespace Application.DTO.BaseDTO
{
    public class BinBaseDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<Guid>? CustomerOrderIds { get; set; }
    }
}
