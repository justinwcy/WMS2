using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteZoneStaffHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteZoneStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteZoneStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundZoneStaff = await wmsDbContext.ZoneStaffs.FirstOrDefaultAsync(
                    zoneStaff => zoneStaff.Id == request.Id,
                    cancellationToken);
                if (foundZoneStaff == null)
                {
                    return GeneralDbResponses.ItemNotFound("ZoneStaff");
                }

                wmsDbContext.ZoneStaffs.Remove(foundZoneStaff);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ZoneStaff");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    