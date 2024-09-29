namespace Domain.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Email { get; set; }
        public List<CustomerOrder> CustomerOrders { get; set; }
    }
}
