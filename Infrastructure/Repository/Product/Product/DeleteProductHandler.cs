using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProduct = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Id,
                    cancellationToken);
                if (foundProduct == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }

                var foundInventory = await wmsDbContext.Inventories.FirstOrDefaultAsync(
                    inventory => inventory.ProductId == request.Id, cancellationToken
                );
                if (foundInventory != null && foundInventory.Quantity != 0)
                {
                    return new ServiceResponse(false, "Cannot be deleted because product still in inventory");
                }

                var foundProductGroupProduct = await wmsDbContext.ProductGroupProducts.FirstOrDefaultAsync(
                    productGroupProduct => productGroupProduct.ProductId == request.Id,
                    cancellationToken);
                if (foundProductGroupProduct != null)
                {
                    wmsDbContext.ProductGroupProducts.Remove(foundProductGroupProduct);
                }

                wmsDbContext.Products.Remove(foundProduct);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Product");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    