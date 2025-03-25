using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductGroupProductsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductGroupProductsByIdsQuery, List<GetProductGroupProductResponseDTO>>
    {
        public async Task<List<GetProductGroupProductResponseDTO>> Handle(GetProductGroupProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productGroupProductsFound = await wmsDbContext.ProductGroupProducts.AsNoTracking()
                .Where(productGroupProduct => request.ProductGroupProductIds.Contains(productGroupProduct.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetProductGroupProductResponseDTO>();
            foreach (var productGroupProductFound in productGroupProductsFound)
            {
                var getProductGroupProductResponseDTO = productGroupProductFound.Adapt<GetProductGroupProductResponseDTO>();
                getProductGroupProductResponseDTO.ProductGroupId = productGroupProductFound.ProductGroupId;
                getProductGroupProductResponseDTO.ProductId = productGroupProductFound.ProductId;
                result.Add(getProductGroupProductResponseDTO);
            }
            
            return result;
        }
    }
}
