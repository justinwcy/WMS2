using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllRefundOrderIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllRefundOrderIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllRefundOrderIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var refundOrderIds = await wmsDbContext.RefundOrders
                .AsNoTracking()
                .Where(refundOrder => staffIdsInCompany.Contains(refundOrder.CreatedBy))
                .Select(refundOrder => refundOrder.Id)
                .ToListAsync(cancellationToken);

            return refundOrderIds;
        }
    }
}
    