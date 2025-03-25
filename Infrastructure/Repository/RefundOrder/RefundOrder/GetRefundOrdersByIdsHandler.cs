using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetRefundOrdersByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRefundOrdersByIdsQuery, List<GetRefundOrderResponseDTO>>
    {
        public async Task<List<GetRefundOrderResponseDTO>> Handle(GetRefundOrdersByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var refundOrdersFound = await wmsDbContext.RefundOrders.AsNoTracking()
                .Where(refundOrder => request.RefundOrderIds.Contains(refundOrder.Id))
                .Include(refundOrder => refundOrder.RefundOrderProducts)
                .ToListAsync(cancellationToken);

            var result = new List<GetRefundOrderResponseDTO>();
            foreach (var refundOrderFound in refundOrdersFound)
            {
                var getRefundOrderResponseDTO = refundOrderFound.Adapt<GetRefundOrderResponseDTO>();
                getRefundOrderResponseDTO.RefundOrderProductIds = refundOrderFound.RefundOrderProducts?
                    .Select(refundOrderProduct => refundOrderProduct.Id).ToList();
                result.Add(getRefundOrderResponseDTO);
            }
            
            return result;
        }
    }
}
