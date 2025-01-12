using Domain.Entities;

namespace Application.DTO.BaseDTO
{
    public class StaffBaseDTO : BaseDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> ZoneIds { get; set; }
    }
}
