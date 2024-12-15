using System.Collections.Generic;

using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllStaffNotificationByStaffIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllStaffNotificationByStaffIdQuery, IEnumerable<GetStaffNotificationResponseDTO>>
    {
        public async Task<IEnumerable<GetStaffNotificationResponseDTO>> Handle(
            GetAllStaffNotificationByStaffIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffNotifications = await wmsDbContext.StaffNotifications
                .AsNoTracking()
                .Where(staffNotification => staffNotification.StaffId == request.StaffId)
                .ToListAsync(cancellationToken: cancellationToken);

            var getStaffNotificationResponseDTOList =
                staffNotifications.Adapt<List<GetStaffNotificationResponseDTO>>();
            return getStaffNotificationResponseDTOList;
        }
    }
}
