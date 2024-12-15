using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetShopByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetShopByIdQuery, GetShopResponseDTO>
    {
        public async Task<GetShopResponseDTO> Handle(GetShopByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var shopFound = await wmsDbContext.Shops.AsNoTracking()
                .FirstAsync(shop=>shop.Id == request.Id, cancellationToken);

            var result = shopFound.Adapt<GetShopResponseDTO>();
            return result;
        }
    }
}
    