using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllZoneByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllZoneByCompanyIdQuery, IEnumerable<GetZoneResponseDTO>>
    {
        public async Task<IEnumerable<GetZoneResponseDTO>> Handle(GetAllZoneByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var zones = await wmsDbContext.Zones
                .AsNoTracking()
                .Where(zone => staffIdsInCompany.Contains(zone.CreatedBy))
                .ToListAsync(cancellationToken);

            var zonesDto = zones.Adapt<List<GetZoneResponseDTO>>();
            return zonesDto;
        }
    }
}
    