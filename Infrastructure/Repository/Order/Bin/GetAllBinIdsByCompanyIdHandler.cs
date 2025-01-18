using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllBinIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllBinIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllBinIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var binIds = await wmsDbContext.Bins
                .AsNoTracking()
                .Where(bin => staffIdsInCompany.Contains(bin.CreatedBy))
                .Select(bin => bin.Id)
                .ToListAsync(cancellationToken);

            return binIds;
        }
    }
}
    