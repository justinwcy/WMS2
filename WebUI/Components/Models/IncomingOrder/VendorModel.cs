namespace WebUI.Components.Models
{
    public class VendorModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Email { get; set; }
        public List<Guid> IncomingOrderIds { get; set; }
    }
}
