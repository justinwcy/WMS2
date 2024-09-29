using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

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

                wmsDbContext.Entry(productShopFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<ProductShop>();
                wmsDbContext.ProductShops.Update(adaptData);
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
    