using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductLocationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductLocationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProductLocation = await wmsDbContext.ProductLocations.FirstOrDefaultAsync(
                    productLocation => productLocation.Id == request.Id,
                    cancellationToken);
                if (foundProductLocation == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductLocation");
                }

                wmsDbContext.ProductLocations.Remove(foundProductLocation);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ProductLocation");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    