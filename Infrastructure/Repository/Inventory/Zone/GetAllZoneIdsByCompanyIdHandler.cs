using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllZoneIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllZoneIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllZoneIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var zoneIds = await wmsDbContext.Zones
                .AsNoTracking()
                .Where(zone => staffIdsInCompany.Contains(zone.CreatedBy))
                .Select(zone => zone.Id)
                .ToListAsync(cancellationToken);

            return zoneIds;
        }
    }
}
    