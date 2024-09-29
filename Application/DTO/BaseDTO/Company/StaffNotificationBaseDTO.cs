namespace Application.DTO.BaseDTO
{
    public class StaffNotificationBaseDTO : BaseDTO
    {
        public Guid StaffId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
    }
}
