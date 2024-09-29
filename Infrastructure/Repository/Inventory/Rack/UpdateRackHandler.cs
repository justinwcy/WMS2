using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateRackCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateRackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var rackFound = await wmsDbContext.Racks.FirstOrDefaultAsync(
                    rack => rack.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (rackFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Rack");
                }

                wmsDbContext.Entry(rackFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Rack>();
                wmsDbContext.Racks.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Rack");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    