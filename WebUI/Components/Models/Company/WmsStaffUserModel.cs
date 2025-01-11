namespace WebUI.Components.Models
{
    public sealed class WmsStaffUserModel : ICloneable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public string ConfirmPassword { get; set; } = "";

        public string RolesString { get; set; } = "";

        public List<string> GetRoleList()
        {
            return RolesString.Split(',')
                .Select(role=>role.Trim())
                .ToList();
        }

        public object Clone()
        {
            return new WmsStaffUserModel
            {
                Id = Id,
                FirstName = FirstName, 
                LastName = LastName, 
                Email = Email, 
                Password = Password, 
                ConfirmPassword = ConfirmPassword,
                RolesString = RolesString
            };
        }
    }
}
