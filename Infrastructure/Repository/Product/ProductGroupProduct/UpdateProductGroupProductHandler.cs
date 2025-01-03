using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductGroupProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductGroupProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductGroupProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productGroupProductFound = await wmsDbContext.ProductGroupProducts.FirstOrDefaultAsync(
                    productGroupProduct => productGroupProduct.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productGroupProductFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductGroupProduct");
                }

                wmsDbContext.Entry(productGroupProductFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<ProductGroupProduct>();
                wmsDbContext.ProductGroupProducts.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductGroupProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    