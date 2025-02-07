namespace WebUI.Components.Models
{
    public sealed class StaffModel : ICloneable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public List<string> Roles { get; set; } = [];

        public Guid? CompanyId { get; set; }
        public List<Guid>? ZoneIds { get; set; } = [];

        public List<Guid> StaffNotificationIds { get; set; } = [];

        public Dictionary<string, string> CustomClaims { get; set; } = new();

        public string FullName => $"{FirstName} {LastName}";

        public object Clone()
        {
            return new StaffModel
            {
                Id = Id,
                FirstName = FirstName, 
                LastName = LastName, 
                Email = Email, 
                Password = Password, 
                ConfirmPassword = ConfirmPassword,
                Roles = Roles,
            };
        }
    }
}
