using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetStaffsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffsByIdsQuery, List<GetStaffResponseDTO>>
    {
        public async Task<List<GetStaffResponseDTO>> Handle(GetStaffsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffsFound = await wmsDbContext.Staffs.AsNoTracking()
                .Where(staff => request.StaffIds.Contains(staff.Id))
                .Include(staff => staff.Zones)
                .Include(staff => staff.StaffNotifications)
                .ToListAsync(cancellationToken);

            var result = new List<GetStaffResponseDTO>();
            foreach (var staffFound in staffsFound)
            {
                var getStaffResponseDTO = staffFound.Adapt<GetStaffResponseDTO>();
                getStaffResponseDTO.ZoneIds = staffFound.Zones?.Select(zone => zone.Id).ToList();
                getStaffResponseDTO.StaffNotificationIds = staffFound
                    .StaffNotifications?
                    .Select(staffNotification => staffNotification.Id)
                    .ToList();
                result.Add(getStaffResponseDTO);
            }
            
            return result;
        }
    }
}
