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
                var ProductRackFound = await wmsDbContext.ProductRacks.FirstOrDefaultAsync(
                    ProductRack => ProductRack.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (ProductRackFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductRack");
                }

                wmsDbContext.Entry(ProductRackFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<ProductRack>();
                wmsDbContext.ProductRacks.Update(adaptData);
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
    