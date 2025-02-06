namespace Application.DTO.BaseDTO
{
    public class CompanyBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public List<Guid>? StaffIds { get; set; }
        public List<Guid>? WarehouseIds { get; set; }
    }
}
