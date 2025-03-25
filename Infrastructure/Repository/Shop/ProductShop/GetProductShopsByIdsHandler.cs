using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductShopsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductShopsByIdsQuery, List<GetProductShopResponseDTO>>
    {
        public async Task<List<GetProductShopResponseDTO>> Handle(GetProductShopsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productShopsFound = await wmsDbContext.ProductShops.AsNoTracking()
                .Where(productShop => request.ProductShopIds.Contains(productShop.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetProductShopResponseDTO>();
            foreach (var productShopFound in productShopsFound)
            {
                var getProductShopResponseDTO = productShopFound.Adapt<GetProductShopResponseDTO>();
                getProductShopResponseDTO.ProductId = productShopFound.ProductId;
                getProductShopResponseDTO.ShopId = productShopFound.ShopId;
                result.Add(getProductShopResponseDTO);
            }
            
            return result;
        }
    }
}
