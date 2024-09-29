namespace Domain.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public List<Staff> Staffs { get; set; }
    }
}
