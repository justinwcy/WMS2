namespace Domain.Entities
{
    public class ZoneStaff : EntityBase
    {
        public Guid? ZoneId { get; set; }
        public Zone? Zone { get; set; }
        public Guid? StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}
