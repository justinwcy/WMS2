using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductGroupProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductGroupProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductGroupProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProductGroupProduct = await wmsDbContext.ProductGroupProducts.FirstOrDefaultAsync(
                    productGroupProduct => productGroupProduct.Id == request.Id,
                    cancellationToken);
                if (foundProductGroupProduct == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductGroupProduct");
                }

                wmsDbContext.ProductGroupProducts.Remove(foundProductGroupProduct);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ProductGroupProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    