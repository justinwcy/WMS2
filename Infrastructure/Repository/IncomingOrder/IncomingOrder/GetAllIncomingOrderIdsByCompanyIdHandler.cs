using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllIncomingOrderIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllIncomingOrderIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllIncomingOrderIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var incomingOrderIds = await wmsDbContext.IncomingOrders
                .AsNoTracking()
                .Where(incomingOrder => staffIdsInCompany.Contains(incomingOrder.CreatedBy))
                .Select(incomingOrder=>incomingOrder.Id)
                .ToListAsync(cancellationToken);

            return incomingOrderIds;
        }
    }
}
    