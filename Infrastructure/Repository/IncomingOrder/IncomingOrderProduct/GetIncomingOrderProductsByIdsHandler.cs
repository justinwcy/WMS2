using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetIncomingOrderProductsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetIncomingOrderProductsByIdsQuery, List<GetIncomingOrderProductResponseDTO>>
    {
        public async Task<List<GetIncomingOrderProductResponseDTO>> Handle(GetIncomingOrderProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var incomingOrderProductsFound = await wmsDbContext.IncomingOrderProducts.AsNoTracking()
                .Where(incomingOrderProduct => request.IncomingOrderProductIds.Contains(incomingOrderProduct.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetIncomingOrderProductResponseDTO>();
            foreach (var incomingOrderProductFound in incomingOrderProductsFound)
            {
                var getIncomingOrderProductResponseDTO = incomingOrderProductFound.Adapt<GetIncomingOrderProductResponseDTO>();
                getIncomingOrderProductResponseDTO.ProductId = incomingOrderProductFound.ProductId;
                getIncomingOrderProductResponseDTO.IncomingOrderId = incomingOrderProductFound.IncomingOrderId;
                result.Add(getIncomingOrderProductResponseDTO);
            }
            
            return result;
        }
    }
}
