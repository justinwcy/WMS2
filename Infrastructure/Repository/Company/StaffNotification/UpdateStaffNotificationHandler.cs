using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateStaffNotificationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateStaffNotificationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateStaffNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var staffNotificationFound = await wmsDbContext.StaffNotifications.FirstOrDefaultAsync(
                    staffNotification => staffNotification.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (staffNotificationFound == null)
                {
                    return GeneralDbResponses.ItemNotFound(request.Model.Subject);
                }

                wmsDbContext.Entry(staffNotificationFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<StaffNotification>();
                wmsDbContext.StaffNotifications.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated(request.Model.Subject);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
