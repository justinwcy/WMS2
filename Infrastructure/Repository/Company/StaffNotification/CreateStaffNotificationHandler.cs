using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CreateStaffNotificationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateStaffNotificationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CreateStaffNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var staffFound = await wmsDbContext.Staffs.FirstOrDefaultAsync(
                    staff => request.Model.StaffId == staff.Id,
                    cancellationToken);
                if (staffFound != null)
                {
                    return GeneralDbResponses.ItemAlreadyExist(staffFound.Id.ToString());
                }

                var data = request.Model.Adapt(new StaffNotification());
                wmsDbContext.StaffNotifications.Add(data);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemCreated(request.Model.Subject);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
