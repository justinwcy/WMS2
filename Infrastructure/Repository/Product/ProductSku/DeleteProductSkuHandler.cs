using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductSkuHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductSkuCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductSkuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProductSku = await wmsDbContext.ProductSkus.FirstOrDefaultAsync(
                    productSku => productSku.Id == request.Id,
                    cancellationToken);
                if (foundProductSku == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductSku");
                }

                wmsDbContext.ProductSkus.Remove(foundProductSku);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ProductSku");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    