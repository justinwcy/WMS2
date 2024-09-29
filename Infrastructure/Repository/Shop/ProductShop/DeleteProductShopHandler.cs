using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductShopCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProductShop = await wmsDbContext.ProductShops.FirstOrDefaultAsync(
                    productShop => productShop.Id == request.Id,
                    cancellationToken);
                if (foundProductShop == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductShop");
                }

                wmsDbContext.ProductShops.Remove(foundProductShop);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ProductShop");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    