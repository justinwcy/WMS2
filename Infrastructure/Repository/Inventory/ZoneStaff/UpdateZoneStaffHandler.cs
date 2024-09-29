using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateZoneStaffHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateZoneStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateZoneStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var zoneStaffFound = await wmsDbContext.ZoneStaffs.FirstOrDefaultAsync(
                    zoneStaff => zoneStaff.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (zoneStaffFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ZoneStaff");
                }

                wmsDbContext.Entry(zoneStaffFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<ZoneStaff>();
                wmsDbContext.ZoneStaffs.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ZoneStaff");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    