using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateInventoryHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateInventoryCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var inventoryFound = await wmsDbContext.Inventories.FirstOrDefaultAsync(
                    inventory => inventory.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (inventoryFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Inventory");
                }

                wmsDbContext.Entry(inventoryFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Inventory>();
                wmsDbContext.Inventories.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Inventory");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    