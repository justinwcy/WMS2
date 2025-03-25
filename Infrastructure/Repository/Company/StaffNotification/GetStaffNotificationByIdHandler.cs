using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetStaffNotificationByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffNotificationByIdQuery, GetStaffNotificationResponseDTO>
    {
        public async Task<GetStaffNotificationResponseDTO> Handle(GetStaffNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffNotificationFound = await wmsDbContext.StaffNotifications
                .AsNoTracking()
                .FirstAsync(staffNotification=> staffNotification.Id == request.StaffNotificationId, 
                    cancellationToken);

            var result = staffNotificationFound.Adapt<GetStaffNotificationResponseDTO>();
            return result;
        }
    }
}
