using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllStaffNotificationIdsByStaffIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllStaffNotificationIdsByStaffIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(
            GetAllStaffNotificationIdsByStaffIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffNotificationIds = await wmsDbContext.StaffNotifications
                .AsNoTracking()
                .Select(staffNotification=>staffNotification.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            return staffNotificationIds;
        }
    }
}
