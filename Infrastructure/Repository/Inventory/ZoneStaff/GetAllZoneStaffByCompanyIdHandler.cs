using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllZoneStaffByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllZoneStaffByCompanyIdQuery, IEnumerable<GetZoneStaffResponseDTO>>
    {
        public async Task<IEnumerable<GetZoneStaffResponseDTO>> Handle(GetAllZoneStaffByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var zoneStaffs = await wmsDbContext.ZoneStaffs
                .AsNoTracking()
                .Where(zoneStaff => staffIdsInCompany.Contains(zoneStaff.CreatedBy))
                .ToListAsync(cancellationToken);

            var zoneStaffsDto = zoneStaffs.Adapt<List<GetZoneStaffResponseDTO>>();
            return zoneStaffsDto;
        }
    }
}
    