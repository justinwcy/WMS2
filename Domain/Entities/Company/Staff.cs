namespace Domain.Entities
{
    public class Staff : EntityBase
    {
        // one to many relationship
        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; }

        public List<StaffNotification>? StaffNotifications { get; set; } = [];

        // many to many relationship
        public List<Zone>? Zones { get; set; } = [];
    }
}
