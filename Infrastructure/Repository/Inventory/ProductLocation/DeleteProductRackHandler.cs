using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductRackCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductRackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProductRack = await wmsDbContext.ProductRacks.FirstOrDefaultAsync(
                    ProductRack => ProductRack.Id == request.Id,
                    cancellationToken);
                if (foundProductRack == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductRack");
                }

                wmsDbContext.ProductRacks.Remove(foundProductRack);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ProductRack");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    