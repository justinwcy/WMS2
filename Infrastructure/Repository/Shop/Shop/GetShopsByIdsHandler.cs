using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetShopsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetShopsByIdsQuery, List<GetShopResponseDTO>>
    {
        public async Task<List<GetShopResponseDTO>> Handle(GetShopsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var shopsFound = await wmsDbContext.Shops.AsNoTracking()
                .Where(shop => request.ShopIds.Contains(shop.Id))
                .Include(shop => shop.Products)
                .ToListAsync(cancellationToken);

            var result = new List<GetShopResponseDTO>();
            foreach (var shopFound in shopsFound)
            {
                var getShopResponseDTO = shopFound.Adapt<GetShopResponseDTO>();
                getShopResponseDTO.ProductIds = shopFound.Products?.Select(product => product.Id).ToList();

                result.Add(getShopResponseDTO);
            }
            
            return result;
        }
    }
}
