using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductGroupProductByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductGroupProductByIdQuery, GetProductGroupProductResponseDTO>
    {
        public async Task<GetProductGroupProductResponseDTO> Handle(GetProductGroupProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productGroupProductFound = await wmsDbContext.ProductGroupProducts.AsNoTracking()
                .FirstAsync(productGroupProduct=>productGroupProduct.Id == request.Id, cancellationToken);

            var result = productGroupProductFound.Adapt<GetProductGroupProductResponseDTO>();
            result.ProductGroupId = productGroupProductFound.ProductGroupId;
            result.ProductId = productGroupProductFound.ProductId;

            return result;
        }
    }
}
    