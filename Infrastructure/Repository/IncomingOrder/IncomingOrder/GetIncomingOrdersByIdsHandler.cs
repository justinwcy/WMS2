using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetIncomingOrdersByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetIncomingOrdersByIdsQuery, List<GetIncomingOrderResponseDTO>>
    {
        public async Task<List<GetIncomingOrderResponseDTO>> Handle(GetIncomingOrdersByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var incomingOrdersFound = await wmsDbContext.IncomingOrders.AsNoTracking()
                .Where(incomingOrder => request.IncomingOrderIds.Contains(incomingOrder.Id))
                .Include(incomingOrder => incomingOrder.IncomingOrderProducts)
                .ToListAsync(cancellationToken);

            var result = new List<GetIncomingOrderResponseDTO>();
            foreach (var incomingOrderFound in incomingOrdersFound)
            {
                var getIncomingOrderResponseDTO = incomingOrderFound.Adapt<GetIncomingOrderResponseDTO>();
                getIncomingOrderResponseDTO.IncomingOrderProductIds = incomingOrderFound.IncomingOrderProducts?
                    .Select(incomingOrderProduct => incomingOrderProduct.Id)
                    .ToList();
                result.Add(getIncomingOrderResponseDTO);
            }
            
            return result;
        }
    }
}
