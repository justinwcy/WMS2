using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllRefundOrderByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllRefundOrderByCompanyIdQuery, IEnumerable<GetRefundOrderResponseDTO>>
    {
        public async Task<IEnumerable<GetRefundOrderResponseDTO>> Handle(GetAllRefundOrderByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var refundOrders = await wmsDbContext.RefundOrders
                .AsNoTracking()
                .Where(refundOrder => staffIdsInCompany.Contains(refundOrder.CreatedBy))
                .ToListAsync(cancellationToken);

            var refundOrdersDto = refundOrders.Adapt<List<GetRefundOrderResponseDTO>>();
            return refundOrdersDto;
        }
    }
}
    