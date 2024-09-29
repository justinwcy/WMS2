namespace Domain.Entities
{
    public class StaffNotification : EntityBase
    {
        // one to many relationship
        public Staff Staff { get; set; }
        public Guid StaffId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
    }
}
