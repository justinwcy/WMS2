using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductGroupHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductGroupCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productGroupFound = await wmsDbContext.ProductGroups.FirstOrDefaultAsync(
                    productGroup => productGroup.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productGroupFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductGroup");
                }

                productGroupFound.Name = request.Model.Name;
                productGroupFound.Description = request.Model.Description;

                var productsToAdd = await wmsDbContext.Products
                    .Where(product => request.Model.ProductIds.Contains(product.Id))
                    .ToListAsync(cancellationToken);
                productGroupFound.Products.RemoveAll(product => !request.Model.ProductIds.Contains(product.Id));
                foreach (var productToAdd in productsToAdd)
                {
                    if (productGroupFound.Products.All(product => product.Id != productToAdd.Id))
                    {
                        productGroupFound.Products.Add(productToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductGroup");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    