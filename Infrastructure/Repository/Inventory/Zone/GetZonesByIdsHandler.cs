using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetZonesByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetZonesByIdsQuery, List<GetZoneResponseDTO>>
    {
        public async Task<List<GetZoneResponseDTO>> Handle(GetZonesByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var zonesFound = await wmsDbContext.Zones.AsNoTracking()
                .Where(zone => request.ZoneIds.Contains(zone.Id))
                .Include(zone => zone.Staffs)
                .Include(zone => zone.Racks)
                .ToListAsync(cancellationToken);

            var result = new List<GetZoneResponseDTO>();
            foreach (var zoneFound in zonesFound)
            {
                var getZoneResponseDTO = zoneFound.Adapt<GetZoneResponseDTO>();
                getZoneResponseDTO.StaffIds = zoneFound.Staffs?.Select(staff => staff.Id).ToList();
                getZoneResponseDTO.RackIds = zoneFound.Racks?.Select(rack => rack.Id).ToList();
                getZoneResponseDTO.WarehouseId = zoneFound.WarehouseId;
                result.Add(getZoneResponseDTO);
            }
            
            return result;
        }
    }
}
