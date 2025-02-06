namespace Domain.Entities
{
    public class Inventory : EntityBase
    {
        public Guid? ProductId { get; set; }

        // one to one relationship
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public int DaysLeadTime { get; set; }
    }
}
