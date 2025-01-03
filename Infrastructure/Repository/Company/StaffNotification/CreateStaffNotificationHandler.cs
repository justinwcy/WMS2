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
        IRequestHandler<CreateStaffNotificationCommand, CreateStaffNotificationResponseDTO>
    {
        public async Task<CreateStaffNotificationResponseDTO> Handle(CreateStaffNotificationCommand request, CancellationToken cancellationToken)
        {
            
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffFound = await wmsDbContext.Staffs.FirstOrDefaultAsync(
                staff => request.Model.StaffId == staff.Id,
                cancellationToken);
            if (staffFound != null)
            {
                throw new Exception(GeneralResponses.ItemAlreadyExist(staffFound.Id.ToString()));
            }

            var data = request.Model.Adapt(new StaffNotification());
            var staffNotificationCreated = wmsDbContext.StaffNotifications.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateStaffNotificationResponseDTO() { Id = staffNotificationCreated.Entity.Id };

        }
    }
}
