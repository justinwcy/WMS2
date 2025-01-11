using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllRefundOrderProductIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllRefundOrderProductIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllRefundOrderProductIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var refundOrderProductIds = await wmsDbContext.RefundOrderProducts
                .AsNoTracking()
                .Where(refundOrderProduct => staffIdsInCompany.Contains(refundOrderProduct.CreatedBy))
                .Select(refundOrderProduct=>refundOrderProduct.Id)
                .ToListAsync(cancellationToken);

            return refundOrderProductIds;
        }
    }
}
    