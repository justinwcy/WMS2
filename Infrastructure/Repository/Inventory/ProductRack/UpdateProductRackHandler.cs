using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductRackCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductRackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productRackFound = await wmsDbContext.ProductRacks.FirstOrDefaultAsync(
                    productRack => productRack.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productRackFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductRack");
                }

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId, cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }

                productRackFound.Product = productFound;
                productRackFound.ProductId = request.Model.ProductId;

                var rackFound = await wmsDbContext.Racks.FirstOrDefaultAsync(
                    rack => rack.Id == request.Model.RackId, cancellationToken);
                if (rackFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Rack");
                }

                productRackFound.Rack = rackFound;
                productRackFound.RackId = request.Model.RackId;

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductRack");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    