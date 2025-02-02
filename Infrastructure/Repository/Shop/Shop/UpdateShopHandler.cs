using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateShopCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var shopFound = await wmsDbContext.Shops.FirstOrDefaultAsync(
                    shop => shop.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (shopFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Shop");
                }

                shopFound.Address = request.Model.Address;
                shopFound.Name = request.Model.Name;
                shopFound.Platform = request.Model.Platform;
                shopFound.Website = request.Model.Website;

                var productsToAdd = await wmsDbContext.Products
                    .Where(product => request.Model.ProductIds.Contains(product.Id))
                    .ToListAsync(cancellationToken);
                shopFound.Products.RemoveAll(product => !request.Model.ProductIds.Contains(product.Id));
                foreach (var productToAdd in productsToAdd)
                {
                    if (shopFound.Products.All(product => product.Id != productToAdd.Id))
                    {
                        shopFound.Products.Add(productToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Shop");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    