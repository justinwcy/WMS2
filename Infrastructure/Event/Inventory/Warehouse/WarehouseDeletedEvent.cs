using MediatR;

namespace Infrastructure.Event
{
    public class WarehouseDeletedEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
