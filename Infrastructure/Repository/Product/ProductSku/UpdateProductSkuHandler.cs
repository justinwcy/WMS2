using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductSkuHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductSkuCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductSkuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productSkuFound = await wmsDbContext.ProductSkus.FirstOrDefaultAsync(
                    productSku => productSku.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productSkuFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductSku");
                }

                wmsDbContext.Entry(productSkuFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<ProductSku>();
                wmsDbContext.ProductSkus.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductSku");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    