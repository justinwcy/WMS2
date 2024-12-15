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
                .FirstAsync(zone=>zone.Id == request.Id, cancellationToken);

            var result = zoneFound.Adapt<GetZoneResponseDTO>();
            return result;
        }
    }
}
    