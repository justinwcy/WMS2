using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateLocationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateLocationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var locationFound = await wmsDbContext.Locations.FirstOrDefaultAsync(
                    location => location.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (locationFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Location");
                }

                wmsDbContext.Entry(locationFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Location>();
                wmsDbContext.Locations.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Location");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    