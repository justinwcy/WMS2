using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductSkuByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductSkuByIdQuery, IEnumerable<GetProductSkuResponseDTO>>
    {
        public async Task<IEnumerable<GetProductSkuResponseDTO>> Handle(GetProductSkuByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productSkuFound = wmsDbContext.ProductSkus.AsNoTracking()
                .Where(productSku=>productSku.Id == request.Id)
                .ToList();

            var result = productSkuFound.Adapt<IEnumerable<GetProductSkuResponseDTO>>();
            return result;
        }
    }
}
    