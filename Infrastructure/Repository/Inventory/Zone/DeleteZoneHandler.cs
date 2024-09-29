using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteZoneHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteZoneCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundZone = await wmsDbContext.Zones.FirstOrDefaultAsync(
                    zone => zone.Id == request.Id,
                    cancellationToken);
                if (foundZone == null)
                {
                    return GeneralDbResponses.ItemNotFound("Zone");
                }

                wmsDbContext.Zones.Remove(foundZone);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Zone");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    