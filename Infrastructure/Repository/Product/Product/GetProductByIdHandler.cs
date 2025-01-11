using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductByIdQuery, GetProductResponseDTO>
    {
        public async Task<GetProductResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productFound = await wmsDbContext.Products.AsNoTracking()
                .Include(product=>product.ProductGroups)
                .Include(product=>product.Shops)
                .FirstAsync(product=>product.Id == request.Id, cancellationToken);

            var result = productFound.Adapt<GetProductResponseDTO>();
            result.ProductGroupIds = productFound.ProductGroups.Select(productGroup => productGroup.Id).ToList();
            result.ShopIds = productFound.Shops.Select(shop => shop.Id).ToList();
            return result;
        }
    }
}
    