using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;
using Infrastructure.Event;
using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateWarehouseHandler(IWmsDbContextFactory<WmsDbContext> contextFactory, IPublisher publisher) : 
        IRequestHandler<CreateWarehouseCommand, CreateWarehouseResponseDTO>
    {
        public async Task<CreateWarehouseResponseDTO> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Warehouse>();
            var warehouseCreated = wmsDbContext.Warehouses.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);

            //var warehouseCreatedEvent = new WarehouseCreatedEvent()
            //{
            //    Id = warehouseCreated.Entity.Id,
            //    Address = request.Model.Address,
            //    CompanyId = request.Model.CompanyId,
            //    CreatedBy = request.Model.CreatedBy,
            //    Name = request.Model.Name,
            //    ZoneIds = request.Model.ZoneIds,
            //};
            //await publisher.Publish(warehouseCreatedEvent, cancellationToken);
            return new CreateWarehouseResponseDTO() { Id = warehouseCreated.Entity.Id};
        }
    }
}
    