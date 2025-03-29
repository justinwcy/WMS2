using MediatR;

namespace Infrastructure.Event
{
    public class WarehouseUpdatedEvent : INotification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid? CompanyId { get; set; }
        public List<Guid>? ZoneIds { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
