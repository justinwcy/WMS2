using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateWarehouseHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateWarehouseCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var warehouseFound = await wmsDbContext.Warehouses.FirstOrDefaultAsync(
                    warehouse => warehouse.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (warehouseFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Warehouse");
                }

                wmsDbContext.Entry(warehouseFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Warehouse>();
                wmsDbContext.Warehouses.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Warehouse");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    