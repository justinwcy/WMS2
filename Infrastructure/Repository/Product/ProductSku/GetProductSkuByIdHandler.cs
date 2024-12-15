using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductSkuByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductSkuByIdQuery, GetProductSkuResponseDTO>
    {
        public async Task<GetProductSkuResponseDTO> Handle(GetProductSkuByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productSkuFound = await wmsDbContext.ProductSkus.AsNoTracking()
                .FirstAsync(productSku=>productSku.Id == request.Id, cancellationToken);

            var result = productSkuFound.Adapt<GetProductSkuResponseDTO>();
            return result;
        }
    }
}
    