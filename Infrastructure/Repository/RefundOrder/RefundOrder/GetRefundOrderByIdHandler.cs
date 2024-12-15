using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetRefundOrderByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRefundOrderByIdQuery, GetRefundOrderResponseDTO>
    {
        public async Task<GetRefundOrderResponseDTO> Handle(GetRefundOrderByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var refundOrderFound = await wmsDbContext.RefundOrders.AsNoTracking()
                .FirstAsync(refundOrder=>refundOrder.Id == request.Id, cancellationToken);

            var result = refundOrderFound.Adapt<GetRefundOrderResponseDTO>();
            return result;
        }
    }
}
    