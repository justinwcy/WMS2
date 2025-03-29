using Application.Caching;
using Application.DTO.Response;

using Infrastructure.Event;
using MediatR;

namespace Infrastructure.Caching
{
    public class WarehouseCacheInvalidationHandler(ICachedService cachedService) :
        INotificationHandler<WarehouseCreatedEvent>,
        INotificationHandler<WarehouseUpdatedEvent>,
        INotificationHandler<WarehouseDeletedEvent>
    {
        public async Task Handle(WarehouseCreatedEvent notification, CancellationToken cancellationToken)
        {
            var warehouseDTO = new GetWarehouseResponseDTO
            {
                CompanyId = notification.CompanyId,
                Address = notification.Address,
                CreatedBy = notification.CreatedBy,
                Id = notification.Id,
                Name = notification.Name,
                ZoneIds = notification.ZoneIds,
            };
            cachedService.Create($"{CacheKeys.Warehouse}-{notification.Id}", warehouseDTO, null);
        }

        public async Task Handle(WarehouseUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var warehouseDTO = new GetWarehouseResponseDTO
            {
                CompanyId = notification.CompanyId,
                Address = notification.Address,
                CreatedBy = notification.CreatedBy,
                Id = notification.Id,
                Name = notification.Name,
                ZoneIds = notification.ZoneIds,
            };
            cachedService.Update($"{CacheKeys.Warehouse}-{notification.Id}", warehouseDTO);
        }

        public async Task Handle(WarehouseDeletedEvent notification, CancellationToken cancellationToken)
        {
            cachedService.Remove($"{CacheKeys.Warehouse}-{notification.Id}");
        }
    }
}
