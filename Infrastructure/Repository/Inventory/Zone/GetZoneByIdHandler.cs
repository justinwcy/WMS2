using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetZoneByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetZoneByIdQuery, GetZoneResponseDTO>
    {
        public async Task<GetZoneResponseDTO> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var zoneFound = await wmsDbContext.Zones.AsNoTracking()
                .Include(zone => zone.Staffs)
                .Include(zone => zone.Racks)
                .FirstAsync(zone=>zone.Id == request.Id, cancellationToken);

            var result = zoneFound.Adapt<GetZoneResponseDTO>();
            result.StaffIds = zoneFound.Staffs?.Select(staff => staff.Id).ToList();
            result.RackIds = zoneFound.Racks?.Select(rack => rack.Id).ToList();
            result.WarehouseId = zoneFound.WarehouseId;

            return result;
        }
    }
}
    