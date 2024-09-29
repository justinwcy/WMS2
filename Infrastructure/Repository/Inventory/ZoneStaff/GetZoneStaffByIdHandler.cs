using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetZoneStaffByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetZoneStaffByIdQuery, GetZoneStaffResponseDTO>
    {
        public async Task<GetZoneStaffResponseDTO> Handle(GetZoneStaffByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var zoneStaffFound = await wmsDbContext.ZoneStaffs.AsNoTracking()
                .FirstAsync(zoneStaff=>zoneStaff.Id == request.Id, cancellationToken);

            var result = zoneStaffFound.Adapt<GetZoneStaffResponseDTO>();
            return result;
        }
    }
}
    