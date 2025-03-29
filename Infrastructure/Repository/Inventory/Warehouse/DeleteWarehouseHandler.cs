using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;
using Infrastructure.Event;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteWarehouseHandler(
        IWmsDbContextFactory<WmsDbContext> contextFactory, 
        IPublisher publisher) : 
        IRequestHandler<DeleteWarehouseCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundWarehouse = await wmsDbContext.Warehouses.FirstOrDefaultAsync(
                    warehouse => warehouse.Id == request.Id,
                    cancellationToken);
                if (foundWarehouse == null)
                {
                    return GeneralDbResponses.ItemNotFound("Warehouse");
                }

                wmsDbContext.Warehouses.Remove(foundWarehouse);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                //var warehouseDeletedEvent = new WarehouseDeletedEvent()
                //{
                //    Id = request.Id,
                //};
                //await publisher.Publish(warehouseDeletedEvent, cancellationToken);

                return GeneralDbResponses.ItemDeleted("Warehouse");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    