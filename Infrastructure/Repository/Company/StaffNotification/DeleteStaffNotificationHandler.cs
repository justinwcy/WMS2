using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteStaffNotificationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteStaffNotificationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteStaffNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundStaffNotification = await wmsDbContext.StaffNotifications.FirstOrDefaultAsync(
                    staffNotification => staffNotification.Id == request.staffNotificationId,
                    cancellationToken);
                if (foundStaffNotification == null)
                {
                    return GeneralDbResponses.ItemNotFound("StaffNotification");
                }

                wmsDbContext.StaffNotifications.Remove(foundStaffNotification);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted(foundStaffNotification.Subject);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
