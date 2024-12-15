using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductShopByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductShopByIdQuery, GetProductShopResponseDTO>
    {
        public async Task<GetProductShopResponseDTO> Handle(GetProductShopByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productShopFound = await wmsDbContext.ProductShops.AsNoTracking()
                .FirstAsync(productShop=>productShop.Id == request.Id, cancellationToken);

            var result = productShopFound.Adapt<GetProductShopResponseDTO>();
            return result;
        }
    }
}
    