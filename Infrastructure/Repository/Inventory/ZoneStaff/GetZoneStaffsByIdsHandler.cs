using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetZoneStaffsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetZoneStaffsByIdsQuery, List<GetZoneStaffResponseDTO>>
    {
        public async Task<List<GetZoneStaffResponseDTO>> Handle(GetZoneStaffsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var zoneStaffsFound = await wmsDbContext.ZoneStaffs.AsNoTracking()
                .Where(zoneStaff => request.ZoneStaffIds.Contains(zoneStaff.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetZoneStaffResponseDTO>();
            foreach (var zoneStaffFound in zoneStaffsFound)
            {
                var getZoneStaffResponseDTO = zoneStaffFound.Adapt<GetZoneStaffResponseDTO>();
                getZoneStaffResponseDTO.ZoneId = zoneStaffFound.ZoneId;
                getZoneStaffResponseDTO.StaffId = zoneStaffFound.StaffId;
                result.Add(getZoneStaffResponseDTO);
            }
            
            return result;
        }
    }
}
