using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Vendor : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<IncomingOrder>? IncomingOrders { get; set; } = [];
    }
}
