using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetStaffNotificationsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffNotificationsByIdsQuery, List<GetStaffNotificationResponseDTO>>
    {
        public async Task<List<GetStaffNotificationResponseDTO>> Handle(GetStaffNotificationsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffNotificationsFound = await wmsDbContext.StaffNotifications.AsNoTracking()
                .Where(staffNotification => request.StaffNotificationIds.Contains(staffNotification.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetStaffNotificationResponseDTO>();
            foreach (var staffNotificationFound in staffNotificationsFound)
            {
                var getStaffNotificationResponseDTO = staffNotificationFound.Adapt<GetStaffNotificationResponseDTO>();
                result.Add(getStaffNotificationResponseDTO);
            }
            
            return result;
        }
    }
}
