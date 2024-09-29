using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteWarehouseHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
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
                return GeneralDbResponses.ItemDeleted("Warehouse");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    