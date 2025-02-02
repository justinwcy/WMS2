using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

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

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId,
                    cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }
                productGroupProductFound.Product = productFound;
                productGroupProductFound.ProductId = request.Model.ProductId;

                var productGroupFound = await wmsDbContext.ProductGroups.FirstOrDefaultAsync(
                    productGroup => productGroup.Id == request.Model.ProductGroupId,
                    cancellationToken);
                if (productGroupFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductGroup");
                }
                productGroupProductFound.ProductGroup = productGroupFound;
                productGroupProductFound.ProductGroupId = request.Model.ProductGroupId;

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
    