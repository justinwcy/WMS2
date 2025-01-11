using Application.Service.Queries;

namespace Application.DTO.BaseDTO.Identity
{
    public class BaseStaffClaimsDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IDictionary<string, string> CustomClaims { get; set; }

        public Guid CompanyId { get; set; }
    }
}
