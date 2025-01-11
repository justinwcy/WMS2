using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllIncomingOrderProductIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllIncomingOrderProductIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllIncomingOrderProductIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var incomingOrderProductIds = await wmsDbContext.IncomingOrderProducts
                .AsNoTracking()
                .Where(incomingOrderProduct => staffIdsInCompany.Contains(incomingOrderProduct.CreatedBy))
                .Select(incomingOrderProduct=>incomingOrderProduct.Id)
                .ToListAsync(cancellationToken);
            return incomingOrderProductIds;
        }
    }
}
    