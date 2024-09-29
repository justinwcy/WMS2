using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteShopCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundShop = await wmsDbContext.Shops.FirstOrDefaultAsync(
                    shop => shop.Id == request.Id,
                    cancellationToken);
                if (foundShop == null)
                {
                    return GeneralDbResponses.ItemNotFound("Shop");
                }

                wmsDbContext.Shops.Remove(foundShop);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Shop");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    