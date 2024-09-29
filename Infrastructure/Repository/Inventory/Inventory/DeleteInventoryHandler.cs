using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteInventoryHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteInventoryCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundInventory = await wmsDbContext.Inventories.FirstOrDefaultAsync(
                    inventory => inventory.Id == request.Id,
                    cancellationToken);
                if (foundInventory == null)
                {
                    return GeneralDbResponses.ItemNotFound("Inventory");
                }

                wmsDbContext.Inventories.Remove(foundInventory);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Inventory");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    