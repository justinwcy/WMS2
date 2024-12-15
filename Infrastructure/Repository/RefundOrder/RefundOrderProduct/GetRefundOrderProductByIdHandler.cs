using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetRefundOrderProductByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRefundOrderProductByIdQuery, GetRefundOrderProductResponseDTO>
    {
        public async Task<GetRefundOrderProductResponseDTO> Handle(GetRefundOrderProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var refundOrderProductFound = await wmsDbContext.RefundOrderProducts.AsNoTracking()
                .FirstAsync(refundOrderProduct=>refundOrderProduct.Id == request.Id, cancellationToken);

            var result = refundOrderProductFound.Adapt<GetRefundOrderProductResponseDTO>();
            return result;
        }
    }
}
    