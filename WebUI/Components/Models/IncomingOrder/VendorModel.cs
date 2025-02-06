namespace WebUI.Components.Models
{
    public class VendorModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Guid>? IncomingOrderIds { get; set; } = [];
    }
}
