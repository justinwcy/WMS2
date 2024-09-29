using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Extensions.Identity
{
    public class WmsStaff : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
    }
}
