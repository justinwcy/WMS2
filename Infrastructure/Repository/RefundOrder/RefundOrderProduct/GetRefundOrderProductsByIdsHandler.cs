using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetRefundOrderProductsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRefundOrderProductsByIdsQuery, List<GetRefundOrderProductResponseDTO>>
    {
        public async Task<List<GetRefundOrderProductResponseDTO>> Handle(GetRefundOrderProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var refundOrderProductsFound = await wmsDbContext.RefundOrderProducts.AsNoTracking()
                .Where(refundOrderProduct => request.RefundOrderProductIds.Contains(refundOrderProduct.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetRefundOrderProductResponseDTO>();
            foreach (var refundOrderProductFound in refundOrderProductsFound)
            {
                var getRefundOrderProductResponseDTO = refundOrderProductFound.Adapt<GetRefundOrderProductResponseDTO>();
                getRefundOrderProductResponseDTO.ProductId = refundOrderProductFound.ProductId;
                getRefundOrderProductResponseDTO.RefundOrderId = refundOrderProductFound.RefundOrderId;
                result.Add(getRefundOrderProductResponseDTO);
            }
            
            return result;
        }
    }
}
