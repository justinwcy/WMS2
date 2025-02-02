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

                var zoneFound = await wmsDbContext.Zones.FirstOrDefaultAsync(
                    zone => zone.Id == request.Model.ZoneId,
                    cancellationToken);
                if (zoneFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Zone");
                }
                zoneStaffFound.Zone = zoneFound;
                zoneStaffFound.ZoneId = request.Model.ZoneId;

                var staffFound = await wmsDbContext.Staffs.FirstOrDefaultAsync(
                    staff => staff.Id == request.Model.StaffId,
                    cancellationToken);
                if (staffFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Staff");
                }
                zoneStaffFound.Staff = staffFound;
                zoneStaffFound.StaffId = request.Model.StaffId;

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
    