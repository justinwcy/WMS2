namespace WebUI.Components.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Email { get; set; }
        public List<Guid> CustomerOrderIds { get; set; } = [];
    }
}
