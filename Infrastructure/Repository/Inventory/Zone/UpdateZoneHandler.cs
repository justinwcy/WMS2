using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateZoneHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateZoneCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var zoneFound = await wmsDbContext.Zones.FirstOrDefaultAsync(
                    zone => zone.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (zoneFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Zone");
                }

                wmsDbContext.Entry(zoneFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Zone>();
                wmsDbContext.Zones.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Zone");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    