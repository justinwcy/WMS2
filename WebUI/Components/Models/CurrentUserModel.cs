namespace WebUI.Components.Models
{
    public class CurrentUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
    }
}
