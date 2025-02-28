using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

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

                inventoryFound.Quantity = request.Model.Quantity;

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId, cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }

                inventoryFound.Product = productFound;
                inventoryFound.ProductId = request.Model.ProductId;

                var rackFound = await wmsDbContext.Racks.FirstOrDefaultAsync(
                    rack => rack.Id == request.Model.RackId, cancellationToken);
                if (rackFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Rack");
                }

                inventoryFound.Rack = rackFound;
                inventoryFound.RackId = request.Model.RackId;

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
    