using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductShopCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productShopFound = await wmsDbContext.ProductShops.FirstOrDefaultAsync(
                    productShop => productShop.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productShopFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductShop");
                }

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId,
                    cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }
                productShopFound.Product = productFound;
                productShopFound.ProductId = request.Model.ProductId;

                var shopFound = await wmsDbContext.Shops.FirstOrDefaultAsync(
                    shop => shop.Id == request.Model.ShopId,
                    cancellationToken);
                if (shopFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Shop");
                }
                productShopFound.Shop = shopFound;
                productShopFound.ShopId = request.Model.ShopId;

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductShop");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    