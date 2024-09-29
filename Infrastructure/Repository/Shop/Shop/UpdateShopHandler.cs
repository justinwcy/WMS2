using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateShopCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var shopFound = await wmsDbContext.Shops.FirstOrDefaultAsync(
                    shop => shop.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (shopFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Shop");
                }

                wmsDbContext.Entry(shopFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Shop>();
                wmsDbContext.Shops.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Shop");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    