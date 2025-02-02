using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

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

                staffNotificationFound.Body = request.Model.Body;
                staffNotificationFound.IsRead = request.Model.IsRead;
                staffNotificationFound.NotificationDate = request.Model.NotificationDate;
                staffNotificationFound.Subject = request.Model.Subject;

                staffNotificationFound.StaffId = request.Model.StaffId;
                var staffFound = await wmsDbContext.Staffs
                    .FirstOrDefaultAsync(staff => staff.Id == request.Model.StaffId, cancellationToken);
                if (staffFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Staff");
                }
                staffNotificationFound.Staff = staffFound;

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
