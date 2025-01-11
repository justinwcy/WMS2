using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllZoneStaffIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllZoneStaffIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllZoneStaffIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var zoneStaffIds = await wmsDbContext.ZoneStaffs
                .AsNoTracking()
                .Where(zoneStaff => staffIdsInCompany.Contains(zoneStaff.CreatedBy))
                .Select(zoneStaff => zoneStaff.Id)
                .ToListAsync(cancellationToken);

            return zoneStaffIds;
        }
    }
}
    