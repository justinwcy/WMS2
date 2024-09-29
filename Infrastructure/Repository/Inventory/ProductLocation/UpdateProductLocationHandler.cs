using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductLocationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductLocationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productLocationFound = await wmsDbContext.ProductLocations.FirstOrDefaultAsync(
                    productLocation => productLocation.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productLocationFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductLocation");
                }

                wmsDbContext.Entry(productLocationFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<ProductLocation>();
                wmsDbContext.ProductLocations.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductLocation");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    